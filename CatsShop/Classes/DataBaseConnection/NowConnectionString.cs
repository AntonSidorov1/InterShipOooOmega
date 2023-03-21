namespace CatsShop.Classes.DataBaseConnection;

/// <summary>
/// Текущая строка подключения
/// </summary>
public static class NowConnectionString
{
    private static DataBaseDatas _datas = DataBaseDatas.GetDataBase();

    /// <summary>
    /// Строка подключения
    /// </summary>
    public static DataBaseDatas ConnectionDatas
    {
        get => _datas;
        set => _datas = value;
    }

    

    //public const string UserPath = "/api/users";
}