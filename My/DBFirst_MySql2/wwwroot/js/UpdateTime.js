function UpdateTime(ObjID) {
    //回傳時間格式： yyyy/MM/dd HH:mm:ss

    const Now = new Date();
    const FormattedStr =
        Now.getFullYear() + "/" +
        String(Now.getMonth() + 1).padStart(2, '0') + "/" +
        String(Now.getDate()).padStart(2, '0') + " " +
        String(Now.getHours()).padStart(2, '0') + ":" +
        String(Now.getMinutes()).padStart(2, '0') + ":" +
        String(Now.getSeconds()).padStart(2, '0');

    document.getElementById(ObjID).innerHTML = FormattedStr;
}