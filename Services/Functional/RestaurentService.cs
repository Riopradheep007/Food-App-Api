using Common.Model.Restaurent;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Crud;
using Services.Interfaces;
using System.IO;

namespace Services.Functional
{
    public  class RestaurentService:IRestaurentService
    {
        private readonly Lazy<IRestaurentDataAccess> _restaurentDataAccess;
        private readonly IConfiguration _Configuration;
        public RestaurentService(Lazy<IRestaurentDataAccess> restaurentDataAccess, IConfiguration Configuration)
        {
            _restaurentDataAccess = restaurentDataAccess;
            _Configuration = Configuration;
        }
        public RestaurentInformaiton GetRestaurentInformation(int id)
        {
            return _restaurentDataAccess.Value.GetRestaurentInformation(id);

        }
        public void AddFood(Food food)
        {
            if (food.Id >= 0)
            {
                int foodId = _restaurentDataAccess.Value.AddFood(food); 
                string filePath = SaveFoodImageIntoFolder(foodId, food);
                _restaurentDataAccess.Value.addFilePathToFood(filePath,foodId,food.Id);
            }
        }
        public void UpdateFood(UpdateFood food)
        { 
           _restaurentDataAccess.Value.UpdateFood(food);
            if (!string.IsNullOrWhiteSpace(food.ImagePath))
            {
                DeleteImage(food.ImagePath);
                UpdateImage(food.ImagePath,food.FoodImage);
            }
         
        }
        public IList<RestaurentFoods> GetAllFoods(int id)
        {
            var result = _restaurentDataAccess.Value.GetAllFoods(id);
            return result;
        }

        public void DeleteFood(int restaurentId, int foodId, string imgPath)
        {
            _restaurentDataAccess.Value.deleteFood(restaurentId,foodId);
            DeleteImage(imgPath);
        }

        public void UpdateRestaurentInformation(RestaurentInformation restaurentInformation)
        {
            string imagePath = string.Empty;
            _restaurentDataAccess.Value.UpdateRestaurentInformation(restaurentInformation);
        }

        private string CreateRestaurentFolder(int restaurentId)
        {
            string imageDataBasePath = _Configuration.GetValue<string>("FilePath:SaveFoodFolder");
            if (!Directory.Exists(imageDataBasePath))
            {
                Directory.CreateDirectory(imageDataBasePath);
            }
            string restaurentFolder = imageDataBasePath + restaurentId;
            if (!Directory.Exists(restaurentFolder))
            {
                Directory.CreateDirectory(restaurentFolder);
            }
            return restaurentFolder;
        }

        private string SaveRestaurentImageIntoFolder(RestaurentInformation restaurentInformation)
        {
            string fullPath = CreateRestaurentFolder(restaurentInformation.RestaurentId) + "\\RestaurentImage";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            string imagePath = fullPath +"\\" +restaurentInformation.RestaurentId + ".jpeg";
            string convertImage = restaurentInformation.RestaurentImage.Replace("data:image/jpeg;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(convertImage);
            File.WriteAllBytes(imagePath, imageBytes);
            return restaurentInformation.RestaurentId + "/RestaurentImage/" + restaurentInformation.RestaurentId + ".jpeg";
        }
        private string SaveFoodImageIntoFolder(int foodId, Food food)
        {
          
            string fullPath = CreateRestaurentFolder(food.Id)+ "\\"+ foodId+".jpeg";
            string convertImage = food.FoodImage.Replace("data:image/jpeg;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(convertImage);
            File.WriteAllBytes(fullPath, imageBytes);
            return food.Id+"/"+foodId+".jpeg";
        }
        private void DeleteImage(string imgPath)
        {
            string imageDataBasePath = _Configuration.GetValue<string>("FilePath:SaveFoodFolder");
            string imgPathConvertor = imgPath.Replace("/", "\\");
            string fullPath = imageDataBasePath + imgPathConvertor;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private void  UpdateImage(string imgPath,string foodImage)
        {
            string imageDataBasePath = _Configuration.GetValue<string>("FilePath:SaveFoodFolder");
            string imgPathConvertor = imgPath.Replace("/", "\\");
            string fullPath = imageDataBasePath + imgPathConvertor;
            string convertImage = foodImage.Replace("data:image/jpeg;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(convertImage);
            File.WriteAllBytes(fullPath, imageBytes);
        }
    }
}
