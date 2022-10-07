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
            window.location.replace("schedule");
        },
        clearFilter: function () {
            this.$refs.filterTable.clearFilter();
        },
        filterTag: function (value, row) {
            return row.state === value;
        },
        chooseSeat: function (ticket_ID, seat, state) {
            console.log(seat)
            if ((seat == "" && state == "未出行")||(seat == ""&&state == "改签")) {
                url = "../Seat/chooseSeat";
                window.name = ticket_ID;
                window.location.href = url;
            }
            else if (seat != "" && state != "未出行")
                alert("该订单已选座！")
            else
                alert("该订单状态错误，选座失败！")
        },
        booksQuery: function () {
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
            axios.post("/Ticket/booksQuery", {
                startDate: s,
                endDate: e
            })
                .then(response => {
                    console.log(response);
                    var data = JSON.parse(response.data).data;
                    console.log(data);
                    _this.tableData = data;
                })
                .catch(error => {
                    console.log(666);
                    this.tableData = [];
                })
        },
        cancel: function (id) {
            this.$confirm('确定删除?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                axios.post("/Ticket/cancelTicket", { ticket_ID: id }).then(response => {
                    console.log(response);
                    if (JSON.parse(response.data).status == 1) {
                        this.$message({
                            type: 'success',
                            message: '删除成功!'
                        });
                        location.reload();
                    }
                    else
                        alert("该订单已被取消，无法再次退订！")
                });
            })
        },
        change: function (id, state) {
            if (state != "未出行") {
                alert("该订单无法改签！");
                return;
            }
            this.$confirm('确定改签?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {

                url = "../Ticket/ChangeTicket?TicketID=" + id;
                window.location.href = url;//跳到改签界面去
            })
        }
    }
});