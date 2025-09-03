//////加入我的旅行口袋////////////////
const addItemToast = document.getElementById('addItemToast');
const toastBootstrap = bootstrap.Toast.getOrCreateInstance(addItemToast);


//localStorage存放的是JSON格式, 但JS所用的是Array
////-------加入購物車////////////////
let arrMyPocket = [];

if (localStorage.getItem("myPocket"))
	arrMyPocket = JSON.parse(localStorage.getItem("myPocket"));

function addMyPocket(roomID, roomName, area, floor) {

	let result = arrMyPocket.find(item => item.RID == roomID);  //如果沒找到,會回傳undefined

	if (result == undefined) {
		$('#addItemToast .toast-body').html(`【${roomName}】已加入我的旅行口袋 <i class="bi bi-heart-fill"></i>`);


		//將房間加入localStorage

		let newItem = {
			RID: roomID,
			RName: roomName,
			Area: area,
			Floor: floor
		}
		arrMyPocket.push(newItem);

		localStorage.setItem("myPocket", JSON.stringify(arrMyPocket));
	}
	else {
		$('#addItemToast .toast-body').html(`【${roomName}】已在旅行口袋中 <i class="bi bi-heart-fill"></i>`);

	}
	toastBootstrap.show();
}



////-------加入購物車////////////////
function shadow1() {
	$('.CartStatus').css({
		'text-shadow': '0 0 10px orange'

	});

	setTimeout(shadow2, 200);

}


function shadow2() {
	$('.CartStatus').css({
		'text-shadow': '0 0 10px white'

	});
	setTimeout(shadow1, 200);
}

let arrMyCart = [];

if (localStorage.getItem("myCart"))
	arrMyCart = JSON.parse(localStorage.getItem("myCart"));

function addMyCart(roomID, roomName, area, floor, price, checkinDate, checkoutDate) {

	let result = arrMyCart.find(item => item.RID == roomID);  //如果沒找到,會回傳undefined

	if (result == undefined) {
		$('#addItemToast .toast-body').html(`【${roomName}】已加入購物車 <i class="bi bi-cart4"></i>`);

		//將房間加入localStorage

		let newItem = {
			RID: roomID,
			RName: roomName,
			Area: area,
			Floor: floor,
			Price: price,
			CheckinDate: checkinDate,
			CheckoutDate: checkoutDate,
			Qty: 1

		}
		arrMyCart.push(newItem);
		CartStatusCheck();
		localStorage.setItem("myCart", JSON.stringify(arrMyCart));
	}
	else {
		$('#addItemToast .toast-body').html(`【${roomName}】已在購物車中 <i class="bi bi-cart4"></i>`);

	}
	toastBootstrap.show();
}

function CartStatusCheck() {
	$('.CartStatus .badge').text(arrMyCart.length);

	if (arrMyCart.length > 0) {
		
		shadow1();
	}
}
CartStatusCheck();
