
function test(value) {
    alert(value);
    debugger;
}

function vueRender(products) {
    $.each(products, function (index, value) {
        value.quantity = 1;
    });
    const vm = new Vue({
        el: "#main",
        data: {
            cart: [],
            products: products,
            search_pattern: '',
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
            },
            removeFromCart(item) {
                this.cart.splice(this.cart.indexOf(item), 1);
            },
            cartDetail() {
                alert(this.cart);
            },
            productQuantity(product){
                if (product.quantity != null) {
                   var quantity = product.quantity;
                } else {
                   var quantity = product.quantity = 1;
                }
                return quantity;
            },
            getItemQuantity(event) {
                var value = $(event.currentTarget).closest(".product-info").find("#product_quantity").text();
                return parseInt(value);
            },
            increaseQuantity(product){
                product.quantity++;
            },
            decreaseQuantity(product){
                if (product.quantity > 1) product.quantity--;
            },
            searchProducts() {
                $.get("/shop/shop/Buscar", { pattern: this.search_pattern })
                  .done(data => {
                      $.each(data, function (index, value) {
                          value.quantity = 1;
                      });
                      this.products = data;
                  }, "json");
                debugger;
            },
            productsAmount() {
                var amount = 0;
                $.each(this.cart, function (index, value) {
                    amount += value.quantity;
                });
                return amount;
            },
            totalPrice() {
                var price = 0.0;
                $.each(this.cart, function (index, value) {
                    price += (value.item.Price * value.quantity);
                });
                return "$" + price;
            }
        }
    });
}