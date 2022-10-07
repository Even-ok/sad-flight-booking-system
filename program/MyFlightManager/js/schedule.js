var b = new Vue({
    el: "#bk",
    data: {
        activeIndex: '1',
        activeIndex2: '1',
        value6: [],
        tableData: []
    },
    methods: {
        changePage: function () {
            window.location.replace("books");
        },
        clearFilter: function () {
            this.$refs.filterTable.clearFilter();
        },
        filterTag: function (value, row) {
            return row.state === value;
        },
        scheduleQuery: function () {
            var s = this.value6[0];
            var e = this.value6[1];
            if (!s) {
                s = new Date();
                s.setFullYear(1000, 1, 1);
            }
            if (!e) {
                e = new Date();
                e.setFullYear(3000, 1, 1);
            }
            var _this = this;
            axios.post("/Ticket/scheduleQuery", {
                startDate: s,
                endDate: e
            })
                .then(response => {
                    var data = JSON.parse(response.data).data;
                    _this.tableData = data;
                })
                .catch(error => {
                    console.log(666);
                })
        }
    }

});