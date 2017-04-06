using JsonXml.Content.Class;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace JsonXml.Content.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        private string tmp;
        public string Tmp
        {
            get { return tmp; }
            set { SetProperty(ref tmp, value); }
        }

        public ICommand JsonToXmlCommand { get; set; }

        public ContentViewModel()
        {
            Tmp = "";
            JsonToXmlCommand = new DelegateCommand<object>(JsonToXml);
        }

        private void JsonToXml(object obj)
        {
            string user = @"{
                'Name': 'James',
                'Age': 30,
                'Enabled': true,
                Roles: [
                    'Publisher',
                    'Administrator'
                    ]
            }";
            XNode node = JsonConvert.DeserializeXNode(user, "Root");

            Tmp = node.ToString();
        }
    }
}
