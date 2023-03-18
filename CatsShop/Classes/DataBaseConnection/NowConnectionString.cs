namespace CatsShop;

public static class NowConnectionString
{
    private static DataBaseDatas _datas = DataBaseDatas.GetDataBase();

    public static DataBaseDatas ConnectionDatas
    {
        get => _datas;
        set => _datas = value;
    }

    

    public const string UserPath = "/api/users";
}