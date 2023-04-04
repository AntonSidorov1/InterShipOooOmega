package com.example.catsshop;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.API.ApiClient;
import com.example.API.ApiClientWithMessage;
import com.example.API.ResultOfAPI;
import com.example.DB.Helper;
import com.example.Model.Cat;
import com.example.Model.GendersList;
import com.example.Users.UsersHelper;

public class CatEditActivity extends AppCompatActivity {

    String doingCatName = "";
    String[] doingCat = new String[]
            {
              "Добавление",
              "Изменение"
            };

    String doCatName = "";
    String[] doCat = new String[]
            {
                    "Добавить",
                    "Изменить"
            };

    String[] did = new String[]
            {
                    "Добавлен",
                    "Изменён"
            };

    String getDoFull(String doNoFull)
    {
        return doNoFull + " котика";
    }

    Button buttonDoCat;
    TextView textViewDoingCat, infoCat;

    Cat cat = new Cat();
    int doID;

    GendersList genders = new GendersList();
    ArrayAdapter gendersAdapter;

    Spinner spinnerGenders;
    EditText editColor, editSpecies, editAge, editPrice;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cat_edit);

        if(!UsersHelper.RoleIsAdmin(this))
        {
            finish();
        }

        Intent i = getIntent();
        doID = i.getIntExtra("do", 0);
        doCatName = getDoFull(doCat[doID]);
        doingCatName = getDoFull(doingCat[doID]);

        textViewDoingCat = findViewById(R.id.textViewCatEditText);
        textViewDoingCat.setText(doingCatName);

        infoCat = findViewById(R.id.textViewEditCatText);

        buttonDoCat = findViewById(R.id.buttonDoingCatText);
        buttonDoCat.setText(doCatName);

        spinnerGenders = findViewById(R.id.spinnerGender);
        gendersAdapter = new ArrayAdapter(this, android.R.layout.simple_list_item_1);
        spinnerGenders.setAdapter(gendersAdapter);

        editColor = findViewById(R.id.editTextColors);
        editColor.setText("");
        editSpecies = findViewById(R.id.editTextSpecies);
        editSpecies.setText("");
        editAge = findViewById(R.id.editTextAge);
        editAge.setText(String.valueOf(0));
        editPrice = findViewById(R.id.editTextPrice);
        editPrice.setText(String.valueOf(0));

        String text = infoCat.getText().toString();
        GetGenders();
        if(doID == 0)
        {
            infoCat.setText(text + " 0");
            cat = new Cat();
        }
        else
        {
            cat = Cat.GetFromJson(i.getStringExtra("cat"));
            infoCat.setText(text + " " + cat.ID);
            GetGenders();
            editColor.setText(cat.Color);
            editSpecies.setText(cat.Species);
            editAge.setText(String.valueOf(cat.Age));
            editPrice.setText(String.valueOf(cat.Price));
        }


    }

    public void Cancel_Click(View v)
    {
        finish();
    }

    public void GetGenders()
    {
        ApiClient api = new ApiClient(this)
        {
            @Override
            public void ready_result(ResultOfAPI res) throws Exception {
                if(res.Code != 200)
                    return;
                genders = new GendersList(res.Body);
                gendersAdapter.clear();
                gendersAdapter.addAll(genders);
                gendersAdapter.notifyDataSetChanged();
                try {
                    int index = genders.indexOf(cat.Gender);
                    spinnerGenders.setSelection(index);
                }
                catch (Exception e)
                {

                }
            }
        };
        api.GET(Helper.GetURL(this).GetURL()+"/cats-genders", false);
    }

    public void RunEdit(View v) {
        String title = doingCatName;
        String doing = doCatName.replace('У', 'у').replace('Д', 'д');
        String did = this.did[doID].replace('У', 'у').replace('Д', 'д');

        ApiClientWithMessage api = new ApiClientWithMessage(this) {
            @Override
            public void GetResult(ResultOfAPI res) {
                finish();
            }
        };
        api.TitleMessage = title;
        api.MessageReady = "Котик успешно " + did;
        api.MessageFail = "Не удалось " + doing;

        int catID = cat.ID;
        cat.Color = editColor.getText().toString();
        cat.Species = editSpecies.getText().toString();
        try {
            cat.Age = Integer.parseInt(editAge.getText().toString());
        } catch (Exception e) {
        }
        try {
            cat.Price = Double.parseDouble(editPrice.getText().toString());
        } catch (Exception e) {
        }

        try {
            cat.SetGenderByIndex(genders, spinnerGenders.getSelectedItemPosition());
        } catch (Exception e) {

        }

        String body = cat.GetJsonWithOutID();
        String url = Helper.GetURL(this).GetURL() + "/cats";

        if (catID < 1) {
            api.POST(url, body, true);
        } else {
            api.PUT(url + "/" + catID, body, true);
        }

    }
}