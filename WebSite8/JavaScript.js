var myJSON_Object;

var xmlHttp;

var arrButtons = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
var flag = 1;
function onLoadJavaScript() {
    try {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    catch (e) {
        try {
            xmlHttp = new XMLHttpRequest();
        }
        catch (e) {
        }
    }

    var url = "Handler.ashx?cmd=load";
    xmlHttp.open("POST", url, true);
    xmlHttp.onreadystatechange = onRequestLoadJavaScript;
    xmlHttp.send();
}
function onRequestLoadJavaScript() {
    if (xmlHttp.readyState == 4) {
        var myJSON_Text = xmlHttp.responseText;
        myJSON_Object = eval(myJSON_Text);
        Init();
    }
}

function Init() {

    if (myJSON_Object[16] != null && myJSON_Object[16].HiddenButton != 0 && document.getElementById(myJSON_Object[16].HiddenButton).value!=0) {
        flag = 0;
        movebutton(10, myJSON_Object[16].HiddenButton.toString());
    }
    var h = 0;
    var w = 0;
    for (var i = 0; i < 16&& flag==1; i++) {
        buttons = document.getElementById(i.toString());
        buttons.style.backgroundColor = "rgb(" + myJSON_Object[i].R + "," + myJSON_Object[i].G + "," + myJSON_Object[i].B + ")";
        buttons.value = myJSON_Object[i].textvalue;
        buttons.style.width = '100px';
        buttons.style.height = '100px';
        buttons.style.position = 'absolute';
        buttons.style.top = (h * 100).toString() + "px";;
        buttons.style.left = (w * 100).toString() + "px";
        w++;


        if (w == 4) {
            h++;
            w = 0;
        }


        if (buttons.value == "0")
            buttons.style.visibility = 'hidden';
        else
            buttons.style.visibility = 'visible';

        buttons.onclick = function () { if (!myClick(this)) return false; };
    }

    if (myJSON_Object[16] != null && myJSON_Object[16].EndGame) {
        if (confirm('You Win! New Game?')) {
            onLoadJavaScript();
        } else {
            window.close();

        }
    }
}

function myClick(myButton ) {

    var url = "Handler.ashx?cmd=Move&ID=" + myButton.id;
    for (var i = 0; i < 16; i++) {
        var myJsonString = JSON.stringify(myJSON_Object[i]);
        url += "&JSON_Object_"+i+"=" + myJsonString;
    }
    xmlHttp.open("POST", url, true);
    xmlHttp.send();
    
}

function movebutton(a, b) {

    var id;
    var myButton = document.getElementById(b);
    for (var i = 0; i < 16; i++) {
        if (myJSON_Object[i].textvalue.toString() == myButton.value)
            id = i;
    }
    var num = parseInt(myButton.id);

    if (id == num) {
        flag = 1;
        return;
    }

    if (a == 0) {
        var val = myButton.value;
        myButton.value = document.getElementById(id).value;
   //     document.getElementById(id).value = val;

        var temp = myButton.id;
        myButton.id = id;
        document.getElementById(id).id = temp;

        flag = 1;
        Init();
        return;
    }

    
    if (num-id==1)
        myButton.style.pixelLeft -= 10; 
    if (num-id==-1)
        myButton.style.pixelLeft += 10;
    if (num-id==4)
        myButton.style.pixelTop -= 10;
    if (num-id==-4)
        myButton.style.pixelTop += 10; 
    window.setTimeout('movebutton(' + (a - 1).toString() + ',' + b + ')', 10);
}


window.onload = onLoadJavaScript;