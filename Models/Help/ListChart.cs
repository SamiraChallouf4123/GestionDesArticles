using System;
using System.Collections.Generic;

namespace GestionDesArticles.Models.Help
{
    public class ListeCart
    {
        public List<Item> Items { get; private set; }

        public static readonly ListeCart Instance;

        // Constructeur statique (singleton)
        static ListeCart()
        {
            Instance = new ListeCart();
            Instance.Items = new List<Item>();
        }

        protected ListeCart() { }

        // Ajouter un produit au panier
        public void AddItem(Product prod)
        {
            bool exists = false;

            foreach (Item a in Items)
            {
                if (a.Prod.ProductId == prod.ProductId)
                {
                    a.quantite++;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Item newItem = new Item(prod)
                {
                    quantite = 1
                };
                Items.Add(newItem);
            }
        }

        // Supprimer complètement un produit du panier
        public void RemoveItem(Product prod)
        {
            Item itemToRemove = null;

            foreach (Item a in Items)
            {
                if (a.Prod.ProductId == prod.ProductId)
                {
                    itemToRemove = a;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        // Décrémenter la quantité d’un produit
        public void SetLessOneItem(Product prod)
        {
            foreach (Item a in Items)
            {
                if (a.Prod.ProductId == prod.ProductId)
                {
                    if (a.quantite > 1)
                    {
                        a.quantite--;
                    }
                    else
                    {
                        RemoveItem(a.Prod);
                    }
                    return;
                }
            }
        }

        // Modifier la quantité d’un produit
        public void SetItemQuantity(Product prod, int quantity)
        {
            if (quantity <= 0)
            {
                RemoveItem(prod);
                return;
            }

            foreach (Item a in Items)
            {
                if (a.Prod.ProductId == prod.ProductId)
                {
                    a.quantite = quantity;
                    return;
                }
            }
        }

        // Réinitialiser le panier
        public void SetToNull()
        {
            Items.Clear();
        }

        // Calculer le sous-total du panier
        public float GetSubTotal()
        {
            float subTotal = 0;
            foreach (Item i in Items)
            {
                subTotal += i.TotalPrice;
            }
            return subTotal;
        }
    }
}
