<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Plata masa cantina</title>
<link rel="stylesheet" href="css/style.css" />
</head>
<body>
    
<?php
require('db.php');
include("auth.php");
    
    
// If form submitted, insert values into the database.
   
if (isset($_POST['username'])){
        // removes backslashes
 $username = stripslashes($_REQUEST['username']);
        //escapes special characters in a string
 $username = mysqli_real_escape_string($con,$username);
    
 $puncte = stripslashes($_REQUEST['puncte']);
 $puncte = mysqli_real_escape_string($con,$puncte);
    
 //Checking is user existing in the database or not
        $query = "SELECT * FROM `users` WHERE username='$username'";
 $result = mysqli_query($con,$query) or die(mysql_error());
 $rows = mysqli_num_rows($result);
        if($rows==1){
     $_SESSION['username'] = $username;
     $_SESSION['puncte'] = $puncte;       
           

            //and password='".md5($password)."'
            
            // Redirect user to masa.php
     header("Location: masa.php");
         }else{
 echo "<div class='form'>
<h3>Username/password is incorrect.</h3>
<br/>Click here to <a href='login.php'>Login</a></div>";
 }
    }else{
    
?>
    
<div class="form">
<p>Plata masa cantina</p>

<form>
Username:<br>
<input type="text" name="username">
<br>
Puncte adaugate (intre 1 si 5):<br>
<input type="number" name="puncte" min="1" max="5">
<br><br>
<input type="submit">
</form>
    
<p><a href="index.php">Home</a></p>
<a href="logout.php">Logout</a>
</div>
<?php } ?>
</body>
</html>