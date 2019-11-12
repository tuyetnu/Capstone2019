using DormyWebService.ViewModels.NotificationViewModels.SendNotification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DormyWebService.Utilities
{
    public class PushNotificationToFirebase
    {
        private static string FCMUrl = "https://fcm.googleapis.com/fcm/send";
        private static string ServerKey = "AAAAB1xRTjQ:APA91bE7LXpG2yfYqANa4Gmstutq1z7UD7I8698GPBj5hqb9ggFGRBVprnmLLlnpMpo_Rv12RNkrg137RLwVIMo3xOra29JXQYXeNxIPZWXgk1VHyePAcP635TRbVRpNhHOc9KpsPzdn";

        //public static string ApproveBookingBody = "Yêu cầu của bạn đã được duyệt, ";
        public async Task PushNotification(string[] diviceTokens, string body)
        {
            var fcmNotification = new FCMNotification()
            {
                notification = new Notification()
                {
                    title = "Dormitory",
                    text = body
                },
                registration_ids = diviceTokens
            };
            string jsonMessage = JsonConvert.SerializeObject(fcmNotification);

            // Create request to Firebase API
            var request = new HttpRequestMessage(HttpMethod.Post, FCMUrl);
            request.Headers.TryAddWithoutValidation("Authorization", "key=" +ServerKey);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
            }
        }
    }
}
