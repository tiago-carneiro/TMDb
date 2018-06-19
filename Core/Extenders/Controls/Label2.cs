using Xamarin.Forms;

namespace TMDb.Core
{
    public class Label2 : Label
    {
        public string Type { get; set; }

        public Label2()
        {
            Style = (Style)Application.Current.Resources["lblDefault"];
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is MovieDetailsWrapper movie)
            {
                FormattedText = new FormattedString();
                switch (Type)
                {
                    case "date":
                        FormattedText.Spans.Add(new Span { Text = Resource.ReleaseDateLabel, FontSize = FontSize});
                        FormattedText.Spans.Add(new Span { Text = movie.ReleaseDate.ToString("d"), FontSize = FontSize, FontAttributes = FontAttributes.Bold });
                        break;
                    case "genre":
                        FormattedText.Spans.Add(new Span { Text = Resource.GenresLabel, FontSize = FontSize });
                        FormattedText.Spans.Add(new Span { Text = movie.Genres, FontSize = FontSize, FontAttributes = FontAttributes.Bold });
                        break;
                }

            }
        }
    }
}
