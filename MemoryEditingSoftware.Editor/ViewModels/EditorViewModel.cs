using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Editor.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MemoryEditingSoftware.Editor.ViewModels
{
    public class EditorViewModel : BindableBase
    {
        #region Properties

        private ObservableCollection<EditorItemView> editorItemViews;
        public ObservableCollection<EditorItemView> EditorItemViews
        {
            get { return editorItemViews; }
            set { SetProperty(ref editorItemViews, value); }
        }

        private ObservableCollection<EditItem> editItemList;
        public ObservableCollection<EditItem> EditItemList
        {
            get { return editItemList; }
            set { SetProperty(ref editItemList, value); }
        }

        private EditItem selectedEditItem;
        public EditItem SelectedEditItem
        {
            get { return selectedEditItem; }
            set { SetProperty(ref selectedEditItem, value); }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                SetProperty(ref address, value);
            }
        }

        private ObservableCollection<string> readWriteCollection;
        public ObservableCollection<string> ReadWriteCollection
        {
            get { return readWriteCollection; }
            set { SetProperty(ref readWriteCollection, value); }
        }

        private string readWrite;
        public string ReadWrite
        {
            get { return readWrite; }
            set
            {
                SetProperty(ref readWrite, value);
                OnReadWriteChanged(readWrite);
            }
        }

        private void OnReadWriteChanged(string readWrite)
        {
            if (readWrite == "Read")
            {
                ReadWriteVisibility = Visibility.Collapsed;
            }
            else
            {
                ReadWriteVisibility = Visibility.Visible;
            }
        }

        private string val;
        public string Val
        {
            get { return val; }
            set
            {
                SetProperty(ref val, value);
            }
        }

        private ObservableCollection<string> loopingCollection;
        public ObservableCollection<string> LoopingCollection
        {
            get { return loopingCollection; }
            set { SetProperty(ref loopingCollection, value); }
        }

        private string looping;
        public string Looping
        {
            get { return looping; }
            set
            {
                SetProperty(ref looping, value);
            }
        }

        private ObservableCollection<string> enterValueCollection;
        public ObservableCollection<string> EnterValueCollection
        {
            get { return enterValueCollection; }
            set { SetProperty(ref enterValueCollection, value); }
        }

        private string enterValue;
        public string EnterValue
        {
            get { return enterValue; }
            set
            {
                SetProperty(ref enterValue, value);
            }
        }

        private bool isGridEnabled;
        public bool IsGridEnabled
        {
            get { return isGridEnabled; }
            set { SetProperty(ref isGridEnabled, value); }
        }

        private Visibility readWriteVisibility;
        public Visibility ReadWriteVisibility
        {
            get { return readWriteVisibility; }
            set { SetProperty(ref readWriteVisibility, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand EditItemSelectedCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<EditItem> UpdateCommand { get; set; }
        public DelegateCommand CreateNewCommand { get; set; }
        /// <summary>
        /// Remove the edit item chosen by the user from everywhere.
        /// </summary>
        public DelegateCommand<EditorItemView> RemoveCommand { get; set; }
        /// <summary>
        /// Apply the modifications the user has made to an edit item.
        /// </summary>
        public DelegateCommand<EditorItemView> ApplyChangesToAnItemCommand { get; set; }

        #endregion

        #region Constructor

        public EditorViewModel()
        {
            if (Project.GetInstance() != null)
            {
                EditItemList = new ObservableCollection<EditItem>();
                EditorItemViews = new ObservableCollection<EditorItemView>();

                RemoveCommand = new DelegateCommand<EditorItemView>(Remove);
                ApplyChangesToAnItemCommand = new DelegateCommand<EditorItemView>(ApplyChangesToAnItem);
                CreateNewCommand = new DelegateCommand(Create);

                if (Project.GetInstance().EditItems != null)
                {
                    foreach (EditItem ei in Project.GetInstance().EditItems)
                    {
                        EditItemList.Add(ei);
                        EditorItemViews.Add(new EditorItemView(ei, ApplyChangesToAnItemCommand, RemoveCommand));
                    }
                }
                else
                {
                    Project.GetInstance().EditItems = new Collection<EditItem>();
                }

                IsGridEnabled = true;
            }
            else
            {
                IsGridEnabled = false;
            }
        }

        private void ApplyChangesToAnItem(EditorItemView editorItemView)
        {
            editItemList.Where(x => x.ID == editorItemView._editItem.ID).Single().Update(editorItemView._editItem);

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Remove an EditItem from the ListView, the collection of EditItems and in the Project.
        /// </summary>
        /// <param name="editorItemViewToRemove"></param>
        private void Remove(EditorItemView editorItemViewToRemove)
        {
            EditItemList.Remove(editorItemViewToRemove._editItem);
            EditorItemViews.Remove(editorItemViewToRemove);
            Project.GetInstance().EditItems.Remove(editorItemViewToRemove._editItem);
        }

        private void Create()
        {
            EditItem editItem = new EditItem();
            editItemList.Add(editItem);
            EditorItemViews.Add(new EditorItemView(editItem, ApplyChangesToAnItemCommand, RemoveCommand));
            Project.GetInstance().EditItems.Add(editItem);
        }

        #endregion

        #region Public Methods

        #endregion

    }
}
