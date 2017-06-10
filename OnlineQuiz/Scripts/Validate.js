
$(document).ready(function (e) {
    var quizname = $('#Quizform :selected').text();
    $("#QuizHeading").text('Welcome to ' + quizname + ' quiz');
})
var removeValue = function (list, value, separator) {
    separator = separator || ",";
    var values = list.split(separator);
    for (var i = 0 ; i < values.length ; i++) {
        if (values[i] == value) {
            values.splice(i, 1);
            return values.join(separator);
        }
    }
    return list;
}
$('.btnpublishquiz').click(function (e) {
    var allchks = $("input[name='EditQuestions']");
    var questions = "";
    for (var i = 0; i < allchks.length; i++) {
        var chk = allchks[i];
        var qid = $("input[name='EditQuestions']")[i].getAttribute('questionid');
        if (chk.checked) {
            if ($("input[name='hid_qids']").val() != undefined && $("input[name='hid_qids']").val() != "")
                questions = $("input[name='hid_qids']").val() + "," + qid;
            else
                questions = qid;
            $("#hid_qids").val(questions);
        }
    }
    quizid = $('#quizid').val();
    $.ajax({
        url: '/Home/UpdateQuizPost',
        type: 'POST',
        data: { quizid: quizid, hid_qids: questions },
        success: function (result) {

        }

    });


});
$("input[name = 'EditQuestions111']").click(function (e) {
    var questions = "";
    if ($(this).prop('checked')) {
        if ($("input[name='hid_qids']").val() != undefined && $("input[name='hid_qids']").val() != "")
            questions = $("input[name='hid_qids']").val() + "," + $(this).attr("questionid");
        else
            questions = $(this).attr("questionid");
    }
    else {
        var temp = removeValue($("input[name='hid_qids']").val(), $(this).attr("questionid"), ',');
        questions = temp;
    }
    $("#hid_qids").val(questions);
});

$("input[name = 'Questions']").click(function (e) {
    var questions = "";
    if ($(this).prop('checked')) {
        if ($("input[name='hid_questions']").val() != undefined && $("input[name='hid_questions']").val() != "")
            questions = $("input[name='hid_questions']").val() + "," + $(this).attr("questionid");
        else
            questions = $(this).attr("questionid");
    }
    else {
        var temp = removeValue($("input[name='hid_questions']").val(), $(this).attr("questionid"), ',');
        questions = temp;
    }
    $("input[name='hid_questions']").val(questions);
});

$(".LoadQuestion").change(function (e) {
    if ($('#Quizform :selected').val() != "") {
        this.form.submit();

    }
    else
        alert("Please choose a different Quiz.")
})

$("#quiz_submit").click(function (e) {
    if ($('#Quizform :selected').val() == "") {
        alert("Please choose a different Quiz and submit");
        return false;
    }
    var r = confirm("Are you sure you want to submit the quiz?");
    if (r == true) {
        $("input[type=radio]").attr('disabled', true)
        $("input[type=text]").attr('disabled', true)
        $("input[type=button]").attr('disabled', true)
        var allradios = $(".radclass");
        var count = 0;
        for (var i = 0; i < allradios.length; i++) {
            if (allradios[i].checked) {
                if (allradios[i].getAttribute('iscorrect') == "true") {
                    count++;
                }
            }
        }

        for (var j = 0; j < $("input[name='answer']").length; j++) {
            var correctanswer = $("input[name='answer']")[j].getAttribute('correctanswer');
            var enteredanswer = $("input[name='answer']")[j].value;
            if (enteredanswer != "") {
                if (correctanswer == enteredanswer) {
                    count++;
                }
            }
        }
        $('.resultdiv').css('display', 'block');
        $("#lbl_result").html(count + " answers are correct..");
    }
})
$(".radismultiyes").click(function (e) {
    if (this.checked == true)
        $('.answeroptions').css('display', 'block')
})
$(".radismultino").click(function (e) {
    if (this.checked == true)
        $('.answeroptions').css('display', 'none')
})
$('#submitqs').click(function () {
    if ($("#IsMultipleChoice")[0].checked == true) {
        if ($("#Option1").val() == "") {
            if ($("#Option2").val() == "")
                if ($("#Option3").val() == "")
                    if ($("#Option4").val() == "") {
                        alert("Please enter options")
                        return false;
                    }
        }
    }
    if ($("#IsMultipleChoice")[0].checked == true) {
        var CorrectAnswer = $("#CorrectAnswer").val();
        if ($("#Option1").val() != CorrectAnswer) {
            if ($("#Option2").val() != CorrectAnswer)
                if ($("#Option3").val() != CorrectAnswer)
                    if ($("#Option4").val() != CorrectAnswer) {
                        alert("Answer does not match with any of the options..")
                        return false;
                    }
        }
    }
})