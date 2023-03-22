namespace CatsShop.Classes.Buy
{
    /// <summary>
    /// Покупка
    /// </summary>
    public class Buy : BuyDatas
    {
        /// <summary>
        /// Клиент, оформивший покупку
        /// </summary>
        public string Client { get => client; set => client = value; }


        string client = "";

        /// <summary>
        /// Дата совершения покупки
        /// </summary>
        public DateTime BuyDate { get => buyDate; set => buyDate = value; }


        DateTime buyDate = DateTime.Now;

        int id = 0;

        /// <summary>
        /// ID покупки
        /// </summary>
        public int ID
        {
            get => id;
            set => id = value;
        }
    }
}
