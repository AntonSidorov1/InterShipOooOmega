using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using CatsShop.Classes.Cats.CatSpecies;

namespace CatsShop.Classes.Cats.CatModel
{
    /// <summary>
    /// Модель котика (полная информация)
    /// </summary>
    public class CatModelFullDatas : CatModel
    {
		/// <summary>
		/// Цвет
		/// </summary>
        public string Color
		{
			get => GetColor().Name;
			set => ColorID = CatColorsList.CatColorsListFromDB().GetColorFromName(value).ID;
		}
		
		/// <summary>
		/// Пол
		/// </summary>
		public string Gender
		{
			get => GetGender().Name;
			set => GenderID = CatGendersList.GetGenderListFromDB().GetGenderFromName(value).ID;
		}

		/// <summary>
		/// Порода
		/// </summary>
		public string Species
		{
			get => GetSpecies().Name;
			set => SpeciesID = CatSpeciesList.GetSpeciesListFromDB().GetSpeciesFromName(value).ID;
		}
		
		public static CatModelFullDatas GetModel(CatModel model)
		{
			CatModelFullDatas modelResult = new CatModelFullDatas();
			try
			{
				modelResult.ID = model.ID;
			}
			catch(Exception e)
			{

			}
			modelResult.ColorID = model.ColorID;
			modelResult.GenderID = model.GenderID;
			modelResult.SpeciesID = model.SpeciesID;
			return modelResult;
		}

		public CatModelDatasName GetModelWithDatasName()
		{
			CatModelDatasName catModel = new CatModelDatasName();
			catModel.Color = Color;
			catModel.Gender = Gender;
			catModel.Species = Species;
			return catModel;
		}

    }
}