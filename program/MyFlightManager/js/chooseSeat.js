var isclicked = false;
var Row = 0;
var Col = 0;
var st = new Vue({
    el: "#seatchoose",
    data: {
        tArray: [],
        // 座位的二维数组,-1为非座位，0为未购座位，1为已选座位(绿色),2为已购座位(红色)
        seatArray: [],
        // 座位行数
        seatRow: 10,
        // 座位列数
        seatCol: 6,
        // 座位尺寸
        seatSize: ''
    },
    methods:
    {
        //选定且购买座位
        buySeat: function () {
            //遍历seatArray，将值为1的座位变为2
            let oldArray = this.seatArray.slice();
            oldArray[Row][Col] = 2;
            this.seatArray = oldArray;
            axios.post("/Seat/pickSeat", {
                ticket_ID: window.name,
                Row: Row,
                Col: Col
            }).
                then(response => {
                    alert("选座成功");
                    window.location.replace("../Ticket/books");
                }).catch(error => {
                    alert("选座失败");
                    window.location.replace("../Ticket/books");
                })
        },
        //处理座位选择逻辑
        handleChooseSeat: function (row, col, flag) {

            let seatValue = this.seatArray[row][col];
            let newArray = this.seatArray;
            //如果是已购座位，直接返回
            if (seatValue === 2) return
            //如果是已选座位点击后变未选
            if (seatValue === 1) {
                Row = 0;
                Col = 0;
                newArray[row][col] = 0;
                isclicked = false;
            } else if (seatValue === 0 && isclicked == false) {
                Row = row;
                Col = col;
                newArray[row][col] = 1;
                isclicked = true;
            }
            //必须整体更新二维数组，Vue无法检测到数组某一项更新,必须slice复制一个数组才行
            this.seatArray = newArray.slice();

        },
        //初始座位数组
        initSeatArray: function () {
            let seatArray = Array(this.seatRow).fill(0).map(() => Array(this.seatCol).fill(0));
            this.seatSize = this.$refs.innerSeatWrapper
                ? parseInt(parseInt(window.getComputedStyle(this.$refs.innerSeatWrapper).width, 10) / this.seatCol, 10)
                : 0;
            this.seatArray = seatArray;
            let seat = this.seatArray.slice();
            let index = 0;
            for (var i of this.tArray) {
                seat[parseInt(index / this.seatCol)][index % this.seatCol] = i;
                index++;
            }
            console.log(seat);
            this.seatArray = seat;
        }
    },

    mounted: function () {
        axios.post("/Seat/freeSeatQuery", {
            ticket_ID: window.name
        })
            .then(response => {
                var data = JSON.parse(response.data);
                this.seatRow = data.row;
                this.seatCol = data.col;
                this.tArray = data.seats;
                this.initSeatArray();
            })
            .catch(error => {
                console.log(666);
            })
    }
})