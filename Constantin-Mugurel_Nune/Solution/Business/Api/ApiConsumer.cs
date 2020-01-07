using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Business.Api
{
    public class ApiConsumer<TEntity> where TEntity : class
    {
        public static IEnumerable<TEntity> ConsumeGet(string ApiControllerName)
        {
            IEnumerable<TEntity> entities = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppConstants.ApiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(ApiControllerName);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TEntity>>();
                    readTask.Wait();

                    entities = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    entities = Enumerable.Empty<TEntity>();

                    //modelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return entities;
        }

        public static TEntity ConsumeGet(string ApiControllerName, int Id)
        {
            TEntity entity = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppConstants.ApiUrl);
                //HTTP GET
                var responseTask = client.GetAsync(ApiControllerName+"/"+Id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TEntity>();
                    readTask.Wait();

                    entity = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    entity = null;

                    //modelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return entity;
        }

        public static HttpResponseMessage ConsumePost(string ApiControllerName, TEntity entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppConstants.ApiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync(ApiControllerName, entity);
                postTask.Wait();

                return postTask.Result;
            }
        }

        public static HttpResponseMessage ConsumeDelete(string ApiControllerName, int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppConstants.ApiUrl);

                //HTTP DELETE
                //var id = (int)typeof(TEntity).GetProperty("Id").GetValue(entity);
                var deleteTask = client.DeleteAsync(ApiControllerName + "/" + Id.ToString());
                deleteTask.Wait();

                return deleteTask.Result;
            }
        }

        public static HttpResponseMessage ConsumePut(string ApiControllerName, TEntity entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppConstants.ApiUrl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync(ApiControllerName, entity);
                putTask.Wait();

                return putTask.Result;
            }
        }
    }
}
