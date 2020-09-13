using E_Guitar_IT_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace E_Guitar_IT_Project.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            string user = User.Identity.GetUserName();
            ShoppingCart shoppingCart;

            if (db.ShoppingCarts.Find(user) == null)
            {
                shoppingCart = new ShoppingCart();
                shoppingCart.UserId = user;
                shoppingCart.cartItems = new List<CartItem>();
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();


            }
            else
            {
                shoppingCart = db.ShoppingCarts.Find(user);
            }

            var result =(from cartItem in db.CartItems
                         where cartItem.CartId==shoppingCart.UserId
                         select new
                         {
                             cartItem.id
                           
                         }).ToList();

            var myView = new ShoppingCartViewModel();
            myView.UserId = User.Identity.GetUserName();

            foreach (var i in result)
            {
                if (db.CartItems.Find(i.id)!=null)
                {
                    CartItem item = db.CartItems.Find(i.id);
                    if (item != null)
                    {
                        myView.CartItems.Add(item);

                    }
                    else
                    {
                        return HttpNotFound();
                    }

                }
            }

            return View(myView);
        }




        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int? guitarId = id;
            //Adding with guitar id
  
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(User.Identity.Name);

            bool contains = false;
            int itemId = 0;

            foreach(var i in shoppingCart.cartItems)
            {
                if (i.GuitarId == guitarId)
                {
                    contains = true;
                    itemId = i.id;
                }
            }
            CartItem newItem;
            if (contains)
            {
                newItem = db.CartItems.Find(itemId);
                newItem.Quantity++;

            }
            else
            {
                newItem = new CartItem();
                Guitar g = db.Guitars.Find(guitarId);
                newItem.Guitar = g;

                newItem.GuitarId = g.id;
                newItem.Guitar = g;
                newItem.DateCreated = DateTime.Now;
                newItem.CartId = shoppingCart.UserId;
                newItem.ShoppingCart = shoppingCart;
                newItem.Quantity = 1;
               

            }

            db.CartItems.AddOrUpdate(newItem);

            shoppingCart.cartItems.Add(newItem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


   
        // POST: ShoppingCart/Delete/5
      
        public ActionResult Delete(int? id)
        {
    
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(User.Identity.Name);
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return RedirectToAction("Index");
            }
            shoppingCart.cartItems.Remove(cartItem);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}