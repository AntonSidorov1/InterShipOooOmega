namespace CatsShop.Classes.DataBaseConnection;

/// <summary>
/// ������� ������ �����������
/// </summary>
public static class NowConnectionString
{
    private static DataBaseDatas _datas = DataBaseDatas.GetDataBase();

    /// <summary>
    /// ������ �����������
    /// </summary>
    public static DataBaseDatas ConnectionDatas
    {
        get => _datas;
        set => _datas = value;
    }

    

    //public const string UserPath = "/api/users";
}