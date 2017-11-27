
function vueRender(cart_products, cart_items) {

    var cart = [];
    if (cart_products != null) $.each(cart_products, function (index, value) {
        value.Quantity = cart_items.find(x => x.ProductId == value.Id).Quantity;
        cart.push(value);
    });
    
    $("#share").jsSocials({
        shares: ["email", "twitter", "facebook", "googleplus", "linkedin", "pinterest", "stumbleupon", "whatsapp"]
    });

    const vm = new Vue({
        el: "#container",
        data: {
            cart: cart_products,
        },
        methods: {
            confirm() {
                var firstname = $("#client_firstname").val();
                var lastname = $("#client_lastname").val();
                var email = $("#client_email").val();
                var city = $("#client_city").val();
                var countryid = $("#CountryId").val()
                $.post("/Shop/ConfirmarCompra", {
                    TotalPrice: this.totalPrice(), ItemCount: this.itemCount(), FirstName: firstname,
                    LastName: lastname, Email: email, CountryId: countryid, City: city
                }).done(function (data) {
                    window.location.href = data
                });
            },
            increaseQuantity(item) {
                item.Quantity++;
                $.post("/Shop/CambiarCantidad", { ProductId: item.Id, Quantity: 1 });
            },
            decreaseQuantity(item) {
                if (item.Quantity > 1) {
                    item.Quantity--;
                    $.post("/Shop/CambiarCantidad", { ProductId: item.Id, Quantity: -1 });
                } 
            },
            itemCount() {
                count = 0;
                if (this.cart != null) $.each(this.cart, function (index, value) {
                    count += value.Quantity;
                });
                return count;
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
                $.post("/Shop/RemoverDelCarro", { ProductId: id });
            }
           
        }
    });
}