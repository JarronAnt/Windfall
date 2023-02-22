using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WindfallEditor.Util;

namespace WindfallEditor.GameProject
{
    [DataContract]
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }


    }

    class NewProject : ViewModels
    {
        //TODO: dont hard code this in the future
        private readonly string _templatePath = @"C:\Users\jarro\OneDrive\Desktop\Code\Windfall\WindfallEditor\ProjectTemplates\";
       



        private string _name = "NewProject";
        public string Name
        {
            get => _name; 
            set {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Name));
                }
            }
        }



        private string _path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\WindfallProject\";
        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Path));
                }
            }
        }


        private ObservableCollection<ProjectTemplate> _projectTemplates = new ObservableCollection<ProjectTemplate>();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates { get;}

        public NewProject()
        {

            ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplates);
            try {
                var templateFiles = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templateFiles != null);

                foreach (var file in templateFiles)
                {
                   var template = Serializer.FromFile<ProjectTemplate>(file);
                    _projectTemplates.Add(template);
                }

            } catch(Exception e) { 
                Debug.WriteLine(e.Message);
                //TODO: Error logging and handling
            }
        }

    }
}
