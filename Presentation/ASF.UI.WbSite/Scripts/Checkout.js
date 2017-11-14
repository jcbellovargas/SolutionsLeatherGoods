
function vueRender(cart_products, cart_items) {

    var cart = [];
    if (cart_products != null) $.each(cart_products, function (index, value) {
        value.Quantity = cart_items.find(x => x.ProductId == value.Id).Quantity;
        cart.push(value);
    });

    const vm = new Vue({
        el: "#container",
        data: {
            cart: cart_products,
        },
        methods: {
            addToCart(item, event) {
                var item_quantity = this.getItemQuantity(event);
                var existingItem = this.cart.find(x => x.item.Id == item.Id);
                if (existingItem != null) {
                    this.cart[this.cart.indexOf(existingItem)].quantity += item_quantity;
                } else {
                    this.cart.push({ item: item, quantity: item_quantity });
                }
                $.post("/Shop/Shop/AgregarAlCarro", { ProductId: item.Id, Price: item.Price, Quantity: item.quantity });
            },
            increaseQuantity(item) {
                item.Quantity++;
                $.post("/Shop/Shop/CambiarCantidad", { ProductId: item.Id, Quantity: 1 });
            },
            decreaseQuantity(item) {
                if (item.Quantity > 1) {
                    item.Quantity--;
                    $.post("/Shop/Shop/CambiarCantidad", { ProductId: item.Id, Quantity: -1 });
                } 
            },
            totalPrice() {
                price = 0.0
                if (this.cart != null) $.each(this.cart, function (index, value) {
                    price += value.Price * value.Quantity;
                });
                return price;
            },
            removeFromCart(id) {
                this.cart.splice(this.cart.findIndex(x => x.Id == id), 1);
                $.post("/Shop/Shop/RemoverDelCarro", { ProductId: id });
            }
           
        }
    });
}