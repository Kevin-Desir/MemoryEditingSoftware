﻿using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryEditingSoftware.Editor.ViewModels
{
    public class EditorViewModel : BindableBase
    {
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

        public DelegateCommand EditItemSelectedCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<EditItem> UpdateCommand { get; set; }
        public DelegateCommand CreateNewCommand { get; set; }
        public DelegateCommand RemoveCommand { get; set; }
        public DelegateCommand UpCommand { get; set; }
        public DelegateCommand DownCommand { get; set; }

        public EditorViewModel()
        {
            EditItemSelectedCommand = new DelegateCommand(EditItemSelected);
            ClearCommand = new DelegateCommand(Clear);
            UpdateCommand = new DelegateCommand<EditItem>(Update);
            CreateNewCommand = new DelegateCommand(Create);
            RemoveCommand = new DelegateCommand(Remove);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);

            EditItemList = new ObservableCollection<EditItem>();

            ReadWriteCollection = new ObservableCollection<string>();
            ReadWriteCollection.Add("Read");
            ReadWriteCollection.Add("Write");

            LoopingCollection = new ObservableCollection<string>();
            LoopingCollection.Add("One time");
            LoopingCollection.Add("Loop");

            EnterValueCollection = new ObservableCollection<string>();
            EnterValueCollection.Add("Yes");
            EnterValueCollection.Add("No");

            if (Project.GetInstance().EditItems != null)
            {
                foreach (EditItem ei in Project.GetInstance().EditItems)
                {
                    EditItemList.Add(ei);
                }
            }
            else
            {
                Project.GetInstance().EditItems = new Collection<EditItem>();
            }


            Clear();

        }

        private void Down()
        {
            throw new NotImplementedException();
            //if (SelectedEditItem != null && SelectedEditItem.ID > 0)
            //{
            //    EditItem high = EditItemList.First<EditItem>(i => i.ID == SelectedEditItem.ID);
            //    EditItem low = EditItemList.First<EditItem>(i => i.ID == SelectedEditItem.ID - 1);
            //    high.ID -= 1;
            //    low.ID += 1;
            //    //EditItemList.RemoveAt(high.ID);
            //    //EditItemList.Insert(high.ID, high);
            //    //EditItemList.RemoveAt(low.ID);
            //    //EditItemList.Insert(low.ID, low);

            //    EditItem ei = Project.GetInstance().EditItems.First<EditItem>(i => i.ID == low.ID).Copy(low);
            //    EditItem lowProject = Project.GetInstance().EditItems.First<EditItem>(i => i.ID == low.ID);
            //    EditItem highProject = Project.GetInstance().EditItems.First<EditItem>(i => i.ID == high.ID);
            //    lowProject.UpdateEditItem(high);
            //    highProject.UpdateEditItem(ei);
            //}
        }

        private void Up()
        {
            throw new NotImplementedException();
            //if (SelectedEditItem != null && SelectedEditItem.ID < EditItemList.Count() - 1)
            //{
            //    EditItem low = EditItemList.First<EditItem>(i => i.ID == SelectedEditItem.ID);
            //    EditItem high = EditItemList.First<EditItem>(i => i.ID == SelectedEditItem.ID + 1);
            //    low.ID += 1;
            //    high.ID -= 1;
            //    EditItemList.RemoveAt(low.ID);
            //    EditItemList.Insert(low.ID, low);
            //    EditItemList.RemoveAt(high.ID);
            //    EditItemList.Insert(high.ID, high);

            //    EditItem ei = Project.GetInstance().EditItems.First<EditItem>(i => i.ID == low.ID).Copy(low);
            //    Project.GetInstance().EditItems.First<EditItem>(i => i.ID == low.ID).UpdateEditItem(high);
            //    Project.GetInstance().EditItems.First<EditItem>(i => i.ID == high.ID).UpdateEditItem(ei);
            //    Project.GetInstance().EditItems.First<EditItem>(i => i.ID == low.ID).ID -= 1;
            //    Project.GetInstance().EditItems.First<EditItem>(i => i.ID == high.ID).ID += 1;
            //}
        }

        private void Remove()
        {
            if (selectedEditItem != null)
            {
                for (int i = SelectedEditItem.ID + 1; i < EditItemList.Count(); i++)
                {
                    EditItem eitem = EditItemList.ElementAt(i);
                    eitem.ID -= 1;

                    EditItemList.RemoveAt(i);

                    EditItemList.Insert(i, eitem);
                }

                EditItem ei = Project.GetInstance().EditItems.First<EditItem>(i => i.ID == SelectedEditItem.ID);
                Project.GetInstance().EditItems.Remove(ei);

                EditItemList.Remove(SelectedEditItem);

                if (EditItemList.Count > 0)
                {
                    this.ID = EditItemList.Last().ID + 1;
                }
                else
                {
                    this.ID = 0;
                }
            }

        }

        private void Create()
        {
            if (ID < EditItemList.Count())
            {
                this.ID = EditItemList.Count();
            }

            EditItem ei = new EditItem()
            {
                ID = this.ID,
                Address = this.Address,
                Name = this.Name,
                Value = this.Val,
                IsRead = ReadWrite.Equals("Read"),
                IsEnterValue = EnterValue.Equals("Yes"),
                IsLoop = Looping.Equals("Loop"),
            };

            EditItemList.Add(ei);

            Project.GetInstance().EditItems.Add(ei);

            this.ID++;
        }

        private void Update(EditItem editItem)
        {
            // NOTE: needs to be improved with new UpdateEditItem method
            if (this.ID < EditItemList.Count())
            {
                EditItem eitem = EditItemList.First<EditItem>(i => i.ID == this.ID);
                eitem.ID = this.ID;
                eitem.Address = this.Address;
                eitem.Name = this.Name;
                eitem.Value = this.Val;
                eitem.IsRead = ReadWrite.Equals("Read");
                eitem.IsEnterValue = EnterValue.Equals("Yes");
                eitem.IsLoop = Looping.Equals("Loop");

                EditItemList.RemoveAt(this.ID);

                EditItemList.Insert(eitem.ID, eitem);

                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).Address = this.Address;
                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).Name = this.Name;
                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).Value = this.Val;
                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).IsRead = ReadWrite.Equals("Read");
                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).IsEnterValue = EnterValue.Equals("Yes");
                Project.GetInstance().EditItems.First<EditItem>(i => i.ID == this.ID).IsLoop = Looping.Equals("Loop");
            }
        }

        private void Clear()
        {
            if (editItemList.Count > 0)
                this.ID = EditItemList.Last().ID + 1;
            else
                this.ID = 0;
            this.Name = "";
            this.Val = "";
            this.Address = "0x";
            this.ReadWrite = "Write";
            this.Looping = "One time";
            this.EnterValue = "No";
        }

        private void EditItemSelected()
        {
            if (SelectedEditItem == null)
            {
                return;
            }

            ID = SelectedEditItem.ID;
            Name = SelectedEditItem.Name;
            Address = SelectedEditItem.Address;
            if (SelectedEditItem.IsRead)
                ReadWrite = "Read";
            else
                ReadWrite = "Write";
            Val = SelectedEditItem.Value;
            if (SelectedEditItem.IsLoop)
                Looping = "Loop";
            else
                Looping = "One time";
            if (SelectedEditItem.IsEnterValue)
                EnterValue = "Yes";
            else
                EnterValue = "No";

        }
    }
}
