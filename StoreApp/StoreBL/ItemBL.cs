using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class ItemBL
    {
        private ItemRepo _repo;

        public ItemBL(ItemRepo newItemRepo)
        {
            _repo = newItemRepo;
        }

        public Item AddItem(Item newItem){
            return _repo.AddItem(newItem);
        }

        public List<Item> GetAllItems(){
            return _repo.GetAllItem();
        }

        public bool ItemExists(Item newItem){
            if (_repo.GetItem(newItem) != null) return true;
            else{
                return false;
            }
        }
    }
}