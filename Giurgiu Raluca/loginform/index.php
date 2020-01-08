<?php
//include auth.php file on all secure pages
include("auth.php");
?>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Welcome Home</title>
<link rel="stylesheet" href="css/style.css" />
</head>
<body>
<div class="form">
<p>Welcome <?php echo $_SESSION['username']; ?>!</p>
<p>Numarul de puncte de mancare pentru tine este : <?php echo $_SESSION['puncte']; ?></p>
<p><a href="dashboard.php">Adaugare puncte pentru cantina</a></p>
    <p><a href="masa.php">Plata masa cantina</a></p>
<a href="logout.php">Logout</a>
</div>
</body>
</html>