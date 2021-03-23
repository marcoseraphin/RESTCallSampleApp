using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RESTSampleApp
{
    public class CompleteStatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CompleteTemplate { get; set; }
        public DataTemplate UnCompleteTemplate { get; set; }

        public CompleteStatusTemplateSelector()
        {
            this.CompleteTemplate = new DataTemplate(typeof(ViewCellCompleted));
            this.UnCompleteTemplate = new DataTemplate(typeof(ViewCellUncompleted));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ToDoItem toDoItem = item as ToDoItem;

            if (toDoItem.Completed)
            {
                return this.CompleteTemplate;
            }

            return this.UnCompleteTemplate;
        }
    }
}
