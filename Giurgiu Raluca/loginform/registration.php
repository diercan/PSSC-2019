<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Registration</title>
<link rel="stylesheet" href="css/style.css" />
</head>
<body>
<?php
require('db.php');
// If form submitted, insert values into the database.
if (isset($_REQUEST['username'])){
        // removes backslashes
 $username = stripslashes($_REQUEST['username']);
        //escapes special characters in a string
 $username = mysqli_real_escape_string($con,$username); 
 $email = stripslashes($_REQUEST['email']);
 $email = mysqli_real_escape_string($con,$email);
 $password = stripslashes($_REQUEST['password']);
 $password = mysqli_real_escape_string($con,$password);
    
 $nume = stripslashes($_REQUEST['nume']);
 $nume = mysqli_real_escape_string($con,$nume);
 $prenume = stripslashes($_REQUEST['prenume']);
 $prenume = mysqli_real_escape_string($con,$prenume);
    
 $facultatea = stripslashes($_REQUEST['facultatea']);
 $facultatea = mysqli_real_escape_string($con,$facultatea);
    
 $nr_matricol = stripslashes($_REQUEST['nr_matricol']);
 $nr_matricol = mysqli_real_escape_string($con,$nr_matricol);
 $puncte = stripslashes($_REQUEST['puncte']);
 $puncte = mysqli_real_escape_string($con,$puncte);
 $trn_date = date("Y-m-d H:i:s");
    
        $query = "INSERT into `users` (username, password, email, trn_date, nume, prenume, puncte, nr_matricol, facultatea)
VALUES ('$username', '".md5($password)."', '$email', '$trn_date', '$nume', '$prenume', '$puncte', '$nr_matricol', '$facultatea')";
        $result = mysqli_query($con,$query);
        if($result){
            echo "<div class='form'>
<h3>You are registered successfully.</h3>
<br/>Click here to <a href='login.php'>Login</a></div>";
        }
    }else{
?>
   
   
<div class="form">
<h1>Registration</h1>
<form name="registration" action="" method="post">
<input type="text" name="username" placeholder="Username" required />
<input type="email" name="email" placeholder="Email" required />
<input type="password" name="password" placeholder="Password" required />
    
<input type="nume" name="nume" placeholder="Nume" required />
<input type="prenume" name="prenume" placeholder="Prenume" required />

<input type="puncte" name="puncte" placeholder="Puncte" required />
    
<input type="facultatea" name="facultatea" placeholder="Facultatea" required />
<input type="nr_matricol" name="nr_matricol" placeholder="Nr_matricol" required />
    
<input type="submit" name="submit" value="Register" />
</form>
</div>
<?php } ?>
</body>
</html>