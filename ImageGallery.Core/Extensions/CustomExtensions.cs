using Xamarin.Forms;

namespace ImageGallery.Core.Extensions
{
    public static class CustomExtensions
    {
        public static View GetViewFromTemplate(this DataTemplate template, object bindingContext)
        {
            object content;

            if (template is DataTemplateSelector templateSelector)
            {
                content = templateSelector.SelectTemplate(bindingContext, null).CreateContent();
            }
            else
            {
                content = template.CreateContent();

                if (bindingContext != null)
                {
                    if (content is BindableObject bindableObject)
                    {
                        bindableObject.BindingContext = bindingContext;
                    }
                }
            }

            return content is View view ? view : ((ViewCell)content).View;
        }
    }
}
