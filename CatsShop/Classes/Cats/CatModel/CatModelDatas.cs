using CatsShop.Classes.Cats.CatColor;
using CatsShop.Classes.Cats.CatsGender.CatGender;
using CatsShop.Classes.Cats.CatSpecies;

namespace CatsShop.Classes.Cats.CatModel;

/// <summary>
/// Модель котика (данные)
/// </summary>
public class CatModelDatas
{
    private int colorID = 0;

    /// <summary>
    /// ID цвета котика
    /// </summary>
    public int ColorID
    {
        get => colorID;
        set => colorID = value;
    }
	
	/// <summary>
    /// цвет котика
    /// </summary>
    public CatColor.CatColor GetColor()
		=> CatColorsList.CatColorsListFromDB().GetColorFromID(ColorID);
	
	/// <summary>
    /// пол котика
    /// </summary>
    public CatGender GetGender()
		=> CatGendersList.GetGenderListFromDB().GetGenderFromID(GenderID);
	
	/// <summary>
    /// Порода котика
    /// </summary>
    public CatSpecies.CatSpecies GetSpecies()
		=> CatSpeciesList.GetSpeciesListFromDB().GetSpeciesFromID(SpeciesID);
	
	
    
    private int genderID = 0;

    /// <summary>
    /// ID пола котика
    /// </summary>
    public int GenderID
    {
        get => genderID;
        set => genderID = value;
    }
    
    private int speciesID = 0;

    /// <summary>
    /// ID пола котика
    /// </summary>
    public int SpeciesID
    {
        get => speciesID;
        set => speciesID = value;
    }

    public CatModelDatas Copy()
    {
        CatModelDatas modelDatas = new CatModelDatas();
        modelDatas.ColorID = ColorID;
        modelDatas.GenderID = GenderID;
        modelDatas.SpeciesID = SpeciesID;
        return modelDatas;
    }

    public CatModel CopyWithID(int id)
    {
        CatModel model = new CatModel();
        model.ColorID = ColorID;
        model.GenderID = GenderID;
        model.SpeciesID = SpeciesID;
        model.ID = id;
        return model;
    }

    public CatModelFullDatas GetFullDatasWithID(int id)
    {
        return CatModelFullDatas.GetModel(CopyWithID(id));
    }
}