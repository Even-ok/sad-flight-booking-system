var table = new Vue({
    el: '#app2',
    data() {
        return {
            msg: '订购机票',
            tableData1: [],
            tableData2: [],
            ticketPrice: '',
            depart_lc: '',
            arrive_lc: '',
            flight_class: '',
            passenger:''
        }
    },
    mounted: function () {
        var _this = this
        var s = location.href

        axios.post("/Ticket/getIdentification", {}).then(res => {
            console.log('res=>', res)
            _this.tableData2 = JSON.parse(res.data).data;
        })
        axios.post("/Ticket/getLocation", { departAirport: decodeURIComponent(getQueryVariable("departport")), arriveAirport: decodeURIComponent(getQueryVariable("arrivalport")) }).then(res => {
            console.log('res=>', res)
            _this.depart_lc = JSON.parse(res.data).data[0].departLocation
            _this.arrive_lc = JSON.parse(res.data).data[0].arriveLocation
            _this.ticketPrice = getQueryVariable("Price")
            _this.flight_class = decodeURIComponent(getQueryVariable("flightClass"))
            _this.tableData1.push({
                flightNo: getQueryVariable("flightNo"), companyName: decodeURIComponent(getQueryVariable("companyName")),
                companyName: decodeURIComponent(getQueryVariable("companyName")), departTime: decodeURIComponent(getQueryVariable("departTime")),
                arrivalTime: decodeURIComponent(getQueryVariable("arrivalTime")), departport: _this.depart_lc, arrivalport: _this.arrive_lc, flightClass: decodeURIComponent(getQueryVariable("flightClass")),
                Price: getQueryVariable("Price")
            });
            if (JSON.parse(res.data).data[0].departLevel == 'h' || JSON.parse(res.data).data[0].arriveLevel == 'h') {
                var msg;
                if (JSON.parse(res.data).data[0].departLevel == 'h' && JSON.parse(res.data).data[0].arriveLevel != 'h')
                    msg = "出发地" + _this.depart_lc + "为疫情风险地区，请注意防疫！";
                else if (JSON.parse(res.data).data[0].departLevel != 'h' && JSON.parse(res.data).data[0].arriveLevel == 'h')
                    msg = "到达地" + _this.arrive_lc + "为疫情风险地区，请注意防疫！";
                else
                    msg = "出发地" + _this.depart_lc + "及到达地" + _this.arrive_lc + "均为疫情风险地区，请注意防疫！"
                alert(msg);
            }
        })

        },
    methods: {

        add_ticket: function () {

            axios.post("/Ticket/addTicket", {
                flightNumber: getQueryVariable("flightNo"), departDate: decodeURIComponent(getQueryVariable("departTime"))
                , price: this.ticketPrice, flight_class:this.flight_class
            })
                .then(res => {
                    console.log(res)
                    alert("购买成功！")
                    window.location.href="/Ticket/books"
                })
        }
    }
})
function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}