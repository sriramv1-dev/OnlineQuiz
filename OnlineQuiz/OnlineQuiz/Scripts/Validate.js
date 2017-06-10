
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

$("input[name = 'EditQuestions']").click(function (e) {
    var questions = "";
    if($(this).prop('checked'))
    {
        if ($("input[name='hid_qids']").val() != undefined && $("input[name='hid_qids']").val() != "")
            questions = $("input[name='hid_qids']").val() + "," + $(this).attr("questionid");
        else
            questions = $(this).attr("questionid");
    }
    else {
        var temp = removeValue($("input[name='hid_qids']").val(), $(this).attr("questionid"), ',');
        questions = temp;
    }
    $("input[name='hid_qids']").val(questions);
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
alert("Page will be loaded with new set of questions")
})

$("#quiz_submit").click(function (e) {
    var allradios = $(".radclass");
    var count = 0;
    for (var i = 0; i < allradios.length; i++) {
        if (allradios[i].checked) {
            if (allradios[i].getAttribute('iscorrect')) {
                count++;
            }
        }
    }

    for (var j = 0; j < $("input[name='answer']").length; j++) {
        var correctanswer=$("input[name='answer']")[j].getAttribute('correctanswer');
        var enteredanswer=$("input[name='answer']")[j].value;
        if (enteredanswer != "")
        {
            if(correctanswer == enteredanswer)
            {
                count++;
            }
        }
    }
    $("#lbl_result").html(count + " answers you made it correct");

})