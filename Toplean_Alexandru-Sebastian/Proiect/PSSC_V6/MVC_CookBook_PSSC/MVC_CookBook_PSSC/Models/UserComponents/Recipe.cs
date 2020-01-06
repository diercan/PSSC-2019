using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;
using MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents;
using MVC_CookBook_PSSC.Models.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MVC_CookBook_PSSC.Models.UserComponents
{
    [Table("recipes")]
    public class Recipe
    {
        private Integer _id;
        private Integer _creatorID;
        private Text _creatorUsername;
        private Text _recipeName;
        private Ingredients _ingredients;
        private List<Ingredients> _ingredientsList = new List<Ingredients>();
        private LongText _preparation;
        private Number _price;
        private Integer _likes;
        private Integer _dislikes;
        private LongText _imageLocation;
        private LongText _videoLocation;
        private LongText _ingredientsAsStringInput;
        private bool _premium = false;
        private bool _premiumRequest = false;
        private Text _tags;

        private User _creator = new User();
        List<String> video = new List<string> { ".avi", ".mp4", ".flv", ".m4v", ".mpeg", ".m4v" };
        List<String> image = new List<string> { ".bmp", ".gif", ".ico", ".jpg", ".jpeg", ".png" };


        public Recipe() {}
        

        

        #region RecipeCreator Define/Get/Set
      
        public void SetUser(User user)
        {
            _creator = user;
        }
        public User GetUser()
        {
            return _creator;
        }
        #endregion

        #region Database Table Recipes
        
        [Key]
        [Column("ID")]
        public int strID { get; set; }

        [Column("IdUser")]
        public int strUserID { get; set; }

        
        
        [StringLength(255,ErrorMessage ="Recipe name is too long!")]
        [Column("Name")]
        public string strRecipeName { get; set; }

        [StringLength(1024,ErrorMessage ="Too many characters! Maximum character limit is 1024")]
        [Column("Ingredients")]
        public string strIngredients { get; set; }
        
        [StringLength(1024, ErrorMessage = "Too many characters! Maximum character limit is 1024")]
        [Column("Comments")]
        public string strPreparation { get; set; }

        [Column("Price")]
        public float strPrice { get; set; }

        [Column("Likes")]
        public int strLikes { get; set; }

        [Column("Dislikes")]
        public int strDislikes { get; set; }

        [Column("ImageLocation")]
        public string strImageLocation { get; set; }

        [Column("VideoLocation")]
        public String strVideoLocation { get; set; }

        [Column("Premium")]
        public bool strIsPremium { get; set; }

        [Column("PremiumRequested")]
        public bool strPremiumRequested { get; set; }

        [Column("Tag1")]
        public String strComboBoxTag1 { get; set; }
        [Column("Tag2")]
        public String strComboBoxTag2 { get; set; }
        [Column("Tag3")]
        public String strComboBoxTag3 { get; set; }
        [Column("CreatorName")]
        public String strCreatorUsername { get; set; }

        [Column("RawIngredients")]
        public String strRawIngredients { get; set; }

        #endregion

        #region Create Recipe and DbObject

        public async Task<int> CreateValueObjectRecipeAsync(IFormCollection form, User creator, String wwwroot)
        {


            if (creator != null)
            {


                //this._id = new Integer(lastID+1);
                _creator = creator;
                this._creatorID = new Integer(creator.GetUserID);
                this._creatorUsername = new Text(creator.strUsername);
                this._recipeName = new Text(form["strRecipeName"]);
                //Ingredients ingredient = new Ingredients(new Text("Hardcoded Recipe.cs line 125"), new Quantity(new Number(10), new MeasuringUnit(new ShortText("idk"))));

                //_ingredientsList.Add(ingredient);
                //this._ingredients = ingredient;
                _ingredientsAsStringInput = new LongText(form["strRawIngredients"].ToString());
                var lines = form["strRawIngredients"].ToString().Split("\r\n");
                for (int i = 0; i < lines.Length; i++)
                {
                    var ingredientName = lines[i].Split(' ')[0];
                    var ingredientQuantity = float.Parse(lines[i].Split(' ')[1]);
                    var ingredientMeasUnit = lines[i].Split(' ')[2];
                    Ingredients ingredient = new Ingredients(new Text(ingredientName), new Quantity(new Number(ingredientQuantity), new MeasuringUnit(new ShortText(ingredientMeasUnit))));
                    AddInexistentIngredient(ingredient);
                }
                this._preparation = new LongText(form["strPreparation"]);
                this._price = new Number(0);
                this._likes = new Integer(0);
                this._dislikes = new Integer(0);


                this._tags = new Text(form["strComboBoxTag1"] + "_" + form["strComboBoxTag2"] + "_" + form["strComboBoxTag3"]);



                var allfiles = form.Files;

                string PremiumRequest = form["strPremium"].ToString();


                if (PremiumRequest == "true")
                {
                    this._premiumRequest = true;
                }

                //var filePath = Path.GetTempFileName();
                if (!Directory.Exists(wwwroot + @"images\" + creator.strUsername+@"\"+this._recipeName.GetText))
                {
                    Directory.CreateDirectory(wwwroot + @"images\" + creator.strUsername + @"\" + this._recipeName.GetText + @"\video");
                    Directory.CreateDirectory(wwwroot + @"images\" + creator.strUsername + @"\" + this._recipeName.GetText + @"\image");
                }
                foreach (var file in allfiles)
                {
                    if (video.Contains(Path.GetExtension(file.FileName)))
                    {
                        var filePath =wwwroot+ @"/images/" + creator.strUsername + @"/" + this._recipeName.GetText + @"/video" + @"/video" + Path.GetExtension(file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {

                            var fileName = file.FileName;
                            await file.CopyToAsync(stream);
                            this._videoLocation = new LongText(filePath.Split(wwwroot)[1]);
                        }
                    }
                    else if (image.Contains(Path.GetExtension(file.FileName)))
                    {
                        var filePath = wwwroot + @"/images/" + creator.strUsername + @"/"  + this._recipeName.GetText + @"/image" + @"/image" + Path.GetExtension(file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {

                            var fileName = file.FileName;
                            await file.CopyToAsync(stream);
                            this._imageLocation = new LongText(filePath.Split(wwwroot)[1]);
                        }
                    }


                }
                this._creatorID = new Integer(creator.GetUserID);
                this._creatorUsername = creator.GetUsername;

                return 1;
            }
            else return 0;

        }
        public void CreateDatabaseObject()
        {
            try {this.strID = this._id.GetInteger;} catch { }
            try{this.strUserID = this._creatorID.GetInteger;}catch { }
            try{this.strRecipeName = this._recipeName.GetText;}catch { }
            try{
                foreach(Ingredients ing in _ingredientsList)
                {
                    this.strIngredients += ing.GetIngredientAsText.GetText + "%";
                }
            
            }catch { }
            try { this.strRawIngredients = this._ingredientsAsStringInput.GetText; } catch { }
            try {this.strPreparation = this._preparation.GetText;}catch { }
            try{this.strPrice = this._price.GetNumber;}catch { }
            try{this.strLikes = this._likes.GetInteger;}catch { }
            try{this.strDislikes = this._dislikes.GetInteger;}catch { }
            try{this.strVideoLocation = this._videoLocation.GetText;}catch { }
            try{this.strImageLocation = this._imageLocation.GetText;}catch { }
            try{this.strIsPremium = this._premium;}catch { }
            try{this.strPremiumRequested = this._premiumRequest;}catch { }
            try{this.strCreatorUsername = _creator.strUsername;}catch { }
            try { this.strComboBoxTag1 = this._tags.GetText.Split('_')[0];
                this.strComboBoxTag2 = this._tags.GetText.Split('_')[1];
                this.strComboBoxTag3 = this._tags.GetText.Split('_')[2];} catch { }
        }
        #endregion

        public void AddInexistentIngredient(Ingredients ingredient)
        {
            if (!IngredientAlreadyInList(ingredient,_ingredientsList))
                _ingredientsList.Add(ingredient);

        }
       
        private bool IngredientAlreadyInList(Ingredients ing, List<Ingredients> list)
        {
            foreach(Ingredients i in list)
            {
                if (ing.GetIngredientName.GetText == i.GetIngredientName.GetText)
                    return true;
            }
            return false;
        }

        
        public String ConvertDbStrToString(String strIn)
        {
            String outStr = "";
            var rows = strIn.Split("%");
            foreach(var row in rows)
            {
                outStr += row.Split("#")[0] + " " + row.Split("#")[1] + " " + row.Split("#")[2] + "\r\n";

            }

            return outStr;
        }

        
        public void RemoveIngredient(Ingredients ingredient)
        {
            if (IngredientAlreadyInList(ingredient, _ingredientsList))
            {
                _ingredientsList.Remove(ingredient);
            }
            else
                throw new InexistentIngredientException("Ingredient does not exist in the current recipe!");
        }
        public void AddIngredients(List<Ingredients> ingredients)
        {
     
            try
            {
                foreach(Ingredients i in ingredients)
                {
                    if (IngredientAlreadyInList(i, _ingredientsList)) 
                    {
                        throw new ExistentIngredientException("This ingredient already exists in the recipe");

                    }
                    else
                    {
                        _ingredientsList.Add(i);
                    }
                }
            }
            catch { }
            

        }

        public Text GetRecipeName { get { return _recipeName; } }
        public List<Ingredients> GetIngredients { get { return _ingredientsList; } }
        public Ingredients GetSpecificIngredient(Text ingredientName)
        {
            foreach(Ingredients i in _ingredientsList)
            {
                if (i.GetIngredientName.GetText == ingredientName.GetText)
                    return i;
            }
            return null;
        }
        
    }
}
