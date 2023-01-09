namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class ListSliderViewModel
    {

        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       
        public ListSliderViewModel(int id, string mainTitle, string content, string buttonName, string buttonRedirectUrl, int order, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            MainTitle = mainTitle;
            Content = content;
            ButtonName = buttonName;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}

