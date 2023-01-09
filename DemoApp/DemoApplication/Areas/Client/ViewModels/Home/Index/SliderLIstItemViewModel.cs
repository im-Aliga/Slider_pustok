namespace DemoApplication.Areas.Client.ViewModels.Home.Index
{
    public class SliderLIstItemViewModel
    {

        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ImageUrl { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public SliderLIstItemViewModel(string mainTitle, string content, string buttonName, string imageUrl, string buttonRedirectUrl, int order)
        {
            MainTitle = mainTitle;
            Content = content;
            ButtonName = buttonName;
            ImageUrl = imageUrl;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
        }
    }
}
