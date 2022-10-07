new Vue({
    el: '#fb',
    created: function () {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == "feedbackID") { var _feedbackID = pair[1]; }
        }
        if (_feedbackID == null)
            return false;
        axios.post("/Feedback/showHandle", {
            feedbackID: _feedbackID
        }).then(response => {
            var this_feedback = JSON.parse(response.data);
            console.log(this_feedback);
            this.feedbackID = this_feedback.feedbackID;
            this.UserID = this_feedback.UserID;
            this.feedbackTime = this_feedback.feedbackTime;
            this.feedbackContent = this_feedback.feedbackContent;
        })
    },
    methods: {
        submit: function () {
            if (this.feedbackID != null) {
                var _this = this;
                var _answer = document.getElementById("fd");
                axios.post("/Feedback/handleResult", {
                    feedbackID: _this.feedbackID, answer: _answer.value
                })
                    .then(response => {
                        var error_code = JSON.parse(response.data)
                        console.log(error_code.error_code);
                        if (error_code.error_code == 0) {
                            alert("回复反馈成功！");
                            window.location.replace("/Feedback/displayFeedback");
                        }
                    })
                    .catch(error => {
                        console.log(666);
                    })
            }
        }
    },
    data() {
        return {
            feedbackID: '',
            UserID: '',
            feedbackTime: '',
            feedbackContent: '',
            tableData: []
        }
    }
})

