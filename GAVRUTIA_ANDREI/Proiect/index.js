//click event
function submit() {
  const id = document.getElementById("PhoneNumber").value;
  //forma id
  if (id.length != 10) {
    var Parent = document.getElementById("ReservationTable");
    for (let i = 0; i <= Parent.rows.length && Parent.rows.length > 1; i++) {
      Parent.deleteRow(1);
    }
  }
  var starCountRef = firebase.database().ref(`Rezervari/${id}`);
  starCountRef.on('value', function (snapshot) {
    var users = snapshot.val()
    if (users != null) {
      var uids = Object.keys(users);
      document.getElementById("ReservationTable").style.display = "block";
      for (let i = 0; i < uids.length; i++) {
        var table = document.getElementById("ReservationTable");
        // Create an empty <tr> element and add it to the 1st position of the table:
        var row = table.insertRow(1);
        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        // Add some text to the new cells:
        cell1.innerHTML = users[uids[i]].nume;
        cell2.innerHTML = users[uids[i]].autor;
      }
    }
  })
}
//get from firebase
var starCountRef = firebase.database().ref('Books');
starCountRef.on('value', function (snapshot) {
  var Parent = document.getElementById("BooksTable");
  console.log(Parent.rows.length)
  for (let i = 0; i <= Parent.rows.length && Parent.rows.length > 1; i++) {
    Parent.deleteRow(1);
    console.log(i)
    console.log(Parent.rows.length)
  }
  var users = snapshot.val()
  if (users != null) {
    var uids = Object.keys(users);
    for (let i = 0; i < uids.length; i++) {
      // Find a <table> element with id="myTable":
      var table = document.getElementById("BooksTable");

      // Create an empty <tr> element and add it to the 1st position of the table:
      var row = table.insertRow(1);

      // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
      var cell1 = row.insertCell(0);
      var cell2 = row.insertCell(1);
      var cell3 = row.insertCell(2);
      var cell4 = row.insertCell(3);
      var button = document.createElement('input');
      button.setAttribute("type", "button");
      button.setAttribute("value", "Rezerva");
      button.setAttribute("class", "button");

      // Add some text to the new cells:
      cell1.innerHTML = users[uids[i]].nume;
      cell2.innerHTML = users[uids[i]].autor;
      cell3.innerHTML = users[uids[i]].stoc;
      cell4.appendChild(button);
      cell4.addEventListener("click", function () {
        // console.log(users[uids[i]]);
        var telefon = prompt("Adauga numar telefon", "");
        if (telefon.length == 10) {
            firebase.database().ref().child(`Rezervari/${telefon}`).push().set({
              nume: users[uids[i]].nume,
              autor: users[uids[i]].autor,
              data: Date(),
              telefon: telefon
            });
            var stoc = users[uids[i]].stoc - 1;
            firebase.database().ref('Books').child(uids[i]).update({
              stoc: stoc
            })
            window.alert("Rezervarea a fost facuta")
        }
        else{
          window.alert("Numar introdus incorect !")
        }
      });
    }
  }
});
