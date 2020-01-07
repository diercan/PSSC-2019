const express = require('express');
const mongoose = require('mongoose');
const bcrypt = require('bcryptjs');
const passport = require('passport');
const loged = require("../helpers/log");
const router = express.Router();


// Load User model
require('../models/User');
const User = mongoose.model('users');


// User Login Route
router.get('/login', (req, res) => {
    loged.Logs("SUCCES: Login page");
    res.render('users/login');
});

// User Register Route
router.get('/register', (req, res) => {
    loged.Logs("SUCCES: Register page");
    res.render('users/register');
});

// Login Form POST
router.post('/login', (req, res, next) => {
    passport.authenticate('local', {
        successRedirect: 'http://localhost:5000/',
        failureRedirect: 'http://localhost:5000/users/login',
        failureFlash: true
    })(req, res, next);
});


// Register Form POST
router.post('/register', (req, res) => {
    let errors = [];

    if (req.body.password != req.body.password2) {
        errors.push({
            text: 'Passwords do no match'
        });
    }
    if (req.body.password.length < 4) {
        errors.push({
            text: 'Password must be at least 4 characters'
        });
    }
    if (errors.length > 0) {
        loged.Logs("FAILED: failed to registerd user.");
        res.status(400).render('users/register', {
            errors: errors,
            name: req.body.name,
            email: req.body.email,
            password: req.body.password,
            password2: req.body.password2
        });
    } else {
        User.findOne({
            email: req.body.email
        }).then(user => {
            if (user) {
                req.flash('error_msg', 'Email already used!');
                res.redirect('http://localhost:5000/users/register');
            } else {
                const newUser = new User({
                    name: req.body.name,
                    email: req.body.email,
                    password: req.body.password
                });
                bcrypt.genSalt(10, (err, salt) => {
                    bcrypt.hash(newUser.password, salt, (err, hash) => {
                        if (err) throw err;
                        newUser.password = hash;
                        newUser.save()
                            .then(user => {
                                req.flash('success_msg', 'You are registered and can now log in!');
                                loged.Logs("SUCCES: user registerd successfuly.");
                                res.redirect('http://localhost:5000/users/login');
                            })
                            .catch(err => {
                                console.log(err);
                                loged.Logs("FAILED: failed to registerd user.");
                                res.status(500).send('Server Error');
                            });
                    });
                });
            };
        });
    }
});


// Logut User
router.get('/logout', (req, res)=>{
    req.logout();
    req.flash('success_msg', 'You are loged out!');
    res.redirect('http://localhost:5000/users/login');
});

module.exports = router;