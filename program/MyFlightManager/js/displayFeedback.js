
    new Vue({
        el: '#hintmes',
        created: function () {
            var _this = this;
            axios.post("/Feedback/showFeedback", { }).then(res => {
        console.log(res)
                //var myflight = JSON.parse(res.data).data;
                console.log(JSON.parse(res.data).data);
                _this.tableData = JSON.parse(res.data).data;
            })
        },
        methods: {
        handleClick(row) {
        console.log(row);
            },
            handleFeedback(feedbackID) {

                if (feedbackID != null) {
                    var url = '/Feedback/handleFeedback?feedbackID=' + feedbackID;
                    window.location.href = url;
                }
            },
            onSubmit() {
        console.log('sbumit!');
            },
            handleDelete(index) {
        console.log(this.tableData[index]);
                this.tableData.splice(index, 1);
            },
            add() {
                const row = { }
                this.tableData.push(row)
            },
            handleEdit(index, row) {
        console.log(index, row);
            }
        },
        data() {
            return {
        hintmes: {
        messageID: ''

                },
                tableData: [{
        feedback_ID :"111",
                    user_ID: "111",
                    feedback_time: "111",
                    feedback_state:"111",
                    }]
            }
        }
    })