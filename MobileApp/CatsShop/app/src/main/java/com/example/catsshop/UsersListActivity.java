package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import com.example.API.ApiClient;
import com.example.API.ApiClientWithMessage;
import com.example.API.ResultOfAPI;
import com.example.Configuration.ChangeEvent;
import com.example.Configuration.FormatClass;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.Model.Cat;
import com.example.Users.Role;
import com.example.Users.User;
import com.example.Users.UsersHelper;
import com.example.Users.UsersList;

import org.json.JSONArray;
import org.json.JSONObject;

public class UsersListActivity extends AppCompatActivity {

    boolean run = true, run1 = true;
    boolean runAccount = true, runCat = true;

    ListView usersList;

    @Override
    public void finish() {
        run = false;
        run1 = false;
        super.finish();
    }

    @Override
    public void startActivityForResult(@NonNull Intent intent, int requestCode) {
        run1 = false;
        super.startActivityForResult(intent, requestCode);
    }


    public Activity GetContext()
    {
        return this;
    }

    void GetDatas()
    {
        runAccount = false;
        ChangeEvent event = new ChangeEvent()
        {
            @Override
            public void Run() {
                if(!UsersHelper.RoleIsAdmin(GetContext()))
                {
                    finish();
                }
            }
        };
        UsersHelper.GetDatas(this, event);
        runAccount = true;
    }

    UsersList users = new UsersList();
    ArrayAdapter<User> usersAdaptar;

    void ListChange()
    {
        usersAdaptar.clear();
        usersAdaptar.addAll(users);
        usersAdaptar.notifyDataSetChanged();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_users_list);

        usersList = findViewById(R.id.listViewUsers);
        usersAdaptar = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1);
        usersList.setAdapter(usersAdaptar);
        usersList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                run1 = false;
                User user = users.get(position);

                AlertDialog.Builder builder = new AlertDialog.Builder(GetContext());
                builder.setCancelable(false);
                builder.setTitle("Пользователь - "+ user.login);
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        run1 = true;
                    }
                });
                builder.setNegativeButton("Удалить", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        DeleteUser(user);
                    }
                });
                AlertDialog dialog = builder.create();
                dialog.setMessage(user.GetInfo());
                dialog.show();
            }
        });


        RunGetUsersFromApi();
    }

    public void DeleteUser(User user)
    {

        AlertDialog.Builder builder = new AlertDialog.Builder(GetContext());
        builder.setTitle("Удаление пользователя");
        builder.setCancelable(false);

        builder.setPositiveButton("Да", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                ApiClientWithMessage api = new ApiClientWithMessage(GetContext())
                {
                    @Override
                    public void GetResult(ResultOfAPI res) {
                        run1 = true;
                    }
                };
                api.TitleMessage = "Удаление пользователя";
                api.MessageReady = "Пользователь успешно удалён";
                api.MessageFail = "Не удалось удалить пользователя";
                api.DELETE(Helper.GetURL(GetContext()).GetURL() + "/users/"+user.login, true);
            }
        });

        builder.setNegativeButton("Нет", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetDatas();
            }
        });

        AlertDialog dialog = builder.create();
        dialog.setCancelable(false);
        dialog.setMessage("Вы действительно хотите удалить пользователя?");
        dialog.show();

    }

    public void Back_Click(View v)
    {
        finish();
    }

    public void AddAdmin_Click(View v)
    {
        if(!UsersHelper.RoleIsAdmin(this))
        {
            finish();
        }
        Intent i = new Intent(this, SignInActivity.class);
        i.putExtra("Doing", "admin");
        i.putExtra("doingText", "Добавление админа");
        i.putExtra("buttonSignIn", "Добавить");
        startActivityForResult(i, 200);
    }


    public void GetUsers(String res)
    {
        runCat = false;

        try {
            users = UsersList.GetFromJson(res);
        } catch (Exception e) {
            e.printStackTrace();
        }

        ListChange();
        runCat = true;
    }

    public void RunGetUsersFromApi()
    {
        Runnable run = new Runnable() {
            @Override
            public void run() {
                GetUsersFromAPI();
            }
        };
        Thread thread = new Thread(run);
        thread.start();
    }

    public void GetUsersFromAPI()
    {
        while(run)
        {
            if(run1)
            {
                if(runAccount) {
                    GetContext().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            GetDatas();
                        }
                    });
                }
                if(runCat) {
                    GetContext().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            ApiClient api = new ApiClient(GetContext()) {
                                @Override
                                public void ready_result(ResultOfAPI res) throws Exception {
                                    if (res.Code != 200)
                                        throw new Exception();
                                    GetUsers(res.Body);
                                }

                                @Override
                                public void on_fail(ResultOfAPI res, String message) {
                                    on_fail(message);
                                }

                                @Override
                                public void on_fail(String req) {
                                    users.clear();

                                    Toast.makeText(GetContext(),
                                            ErrorMessage(),
                                            Toast.LENGTH_SHORT);
                                    finish();
                                }
                            };
                            api.GET(Helper.GetURL(GetContext()).GetURL() + "/users", true);
                        }
                    });
                }

                try {
                    Thread.sleep(2000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }

            }
        }
    }

}