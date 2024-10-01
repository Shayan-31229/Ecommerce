using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Data
{
    

    public static class FlashHelper
    {
        public static void Flash(this Controller controller, string message, string level = "success")
        {
            // Define the key for TempData
            string key = string.Format("flash-{0}", level.ToLower());

            // Retrieve the existing messages from TempData or create a new list
            List<string> messages;
            if (controller.TempData.ContainsKey(key))
            {
                // TempData stores serialized objects, so you need to safely cast to List<string>
                messages = controller.TempData[key] as List<string> ?? new List<string>();
            }
            else
            {
                messages = new List<string>();
            }

            // Add the new message to the list
            messages.Add(message);

            // Store the updated list back into TempData
            controller.TempData[key] = messages;
        }
    }
}
