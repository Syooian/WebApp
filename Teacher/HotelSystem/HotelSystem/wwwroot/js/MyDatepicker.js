
///這裡是萬年曆的介面
$(".datepicker").datepicker({
	dateFormat: "yy-mm-dd",
	//minDate:1,
	dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
	monthNames: ["一月", "二月", "三月", "四月", "五月", "六月",
		"七月", "八月", "九月", "十月", "十一月", "十二月"],

});  //先初始化datepicker



$(".datepicker").on('click', function () {

	$(this).datepicker('show');
});