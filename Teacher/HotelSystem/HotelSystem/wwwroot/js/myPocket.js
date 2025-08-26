//////加入我的旅行口袋////////////////
const addItemToast = document.getElementById('addItemToast');
const toastBootstrap = bootstrap.Toast.getOrCreateInstance(addItemToast);


//localStorage存放的是JSON格式, 但JS所用的是Array

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

