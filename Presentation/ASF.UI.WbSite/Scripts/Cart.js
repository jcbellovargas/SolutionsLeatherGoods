
function vueRender(products, cart_data, page_count) {
    $.each(products, function (index, value) {
        value.quantity = 1;
    });


    var cart = [];
    if (cart_data != null) $.each(cart_data, function (index, value) {
        value = { item: { Price: value.Price, Id: value.ProductId }, quantity: value.Quantity };
        cart.push(value);
    });

    $("#share").jsSocials({
        shares: ["email", "twitter", "facebook", "googleplus", "linkedin", "pinterest", "stumbleupon", "whatsapp"]
    });


  
    Vue.component('pagination', {
        template: '#pagination',
        props: ['total', 'limit', 'current'],
        data: function () {
            return {
                //show: false
            };
        },
        computed: {
            pages: function () {
                var pages = [];

                for (var i = 1; i <= this.total; i++) {
                    pages.push(i);
                }

                return pages;
            },
            links: function () {
                var first = [1, '...'],
                    last = ['...', this.total],
                    range = [];

                if (this.current <= this.limit) {
                    range = this.range(1, this.limit + 1);

                    return (this.current + range.length) <= this.total ? range.concat(last) : range;
                } else if (this.current > (this.total - this.limit)) {
                    range = this.range(this.total - (this.limit), this.total);

                    return (this.current - range.length) >= 1 ? first.concat(range) : range;
                } else {
                    range = this.range(this.current - Math.ceil(this.limit / 2), this.current + Math.ceil(this.limit / 2));

                    return first.concat(range).concat(last);
                }
            },
            next: function () {
                var next = this.current + 1;

                return next <= this.total ? next : false;
            },
            prev: function () {
                return this.current - 1;
            },
            show: function () {
                return this.next || this.prev;
            }
        },
        methods: {
            range: function (start, end) {
                var pages = [];

                for (var i = start - 1; i < end; i++) {
                    if (this.pages[i]) {
                        pages.push(this.pages[i]);
                    }
                }

                return pages;
            },
            go: function (page) {
                if (isNaN(page)) {
                    return;
                }
                vm.getPage(page);
                this.$dispatch('paginate:to', page);
            }
        }
    });

    var vm = new Vue({
        el: "#container",
        data () {
            return {
                cart: cart,
                products: products,
                search_pattern: '',
                total: page_count,
                current: 1,
                limit: 4
            }
        },
        methods: {
            setCurrent: function (page) {
                this.current = page;
            },
            addToCart(item, event) {
                var item_quantity = this.getItemQuantity(event);
                var existingItem = this.cart.find(x => x.item.Id == item.Id);
                if (existingItem != null) {
                    this.cart[this.cart.indexOf(existingItem)].quantity += item_quantity;
                } else {
                    this.cart.push({ item: item, quantity: item_quantity });
                }
                $.post("/Shop/AgregarAlCarro", { ProductId: item.Id, Price: item.Price, Quantity: item.quantity });
            },
            removeFromCart(item) {
                this.cart.splice(this.cart.indexOf(item), 1);
            },
            cartDetail() {
                alert(this.cart);
            },
            productQuantity(product) {
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
            increaseQuantity(product) {
                product.quantity++;
            },
            decreaseQuantity(product) {
                if (product.quantity > 1) product.quantity--;
            },
            searchProducts() {
                $.get("/Shop/Buscar", { pattern: this.search_pattern })
                  .done(data => {
                      $.each(data[1]["Value"], function (index, value) {
                          value.quantity = 1;
                      });
                      this.products = data[1]["Value"];
                      this.total = data[0]["Value"];
                      //this.products = data;
                  }, "json");
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
            },
            showCartDetail() {
                //showModal = true;
            },
            getPage(page) {
                $.get("/Shop/ObtenerPagina", { pag: page, pattern: this.search_pattern })
                  .done(data => {
                      $.each(data[1]["Value"], function (index, value) {
                          value.quantity = 1;
                      });
                      this.products = data[1]["Value"];
                      this.total = data[0]["Value"];
                  }, "json");
            }
        }
    });




}