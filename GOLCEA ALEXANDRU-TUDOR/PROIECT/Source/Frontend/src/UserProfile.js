const axios = require('axios');

var UserProfile = (function() {
  var auth = localStorage.getItem("user");
  // if(auth !== null){
  //   axios.get(`${process.env.REACT_APP_API_URL}/user/` + auth.id + '.' + auth.isDoctor)
  //   .then((response) => {
  //   })
  //   .catch(function (error) {
  //     localStorage.setItem("user", null);
  //     auth = null;
  //   });
  // }

  var getAuth = function(){
      console.log("auth", auth);
    return auth;
  }

  var setAuth = function(user){
    if (user !== null){
      window.location.href = `/dashboard/calendar`;
      localStorage.setItem("user", user); 
      localStorage.setItem("id", user.id);
    } else {
      console.log("redirect");
      localStorage.clear();
      window.location.href = `/auth/login`;
    }


    
    // localStorage.setItem("user", JSON.stringify(authenticated));
    // if(authenticated.isAdmin){
    //   window.location.href = `/admin/dashboard`;
    // }
    // else{
    //   window.location.href = `/admin/dashboard`;
    // }
  }

  return {
    getAuth: getAuth,
    setAuth: setAuth
  }

})();

export default UserProfile;
