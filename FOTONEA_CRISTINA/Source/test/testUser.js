process.env.NODE_ENV = 'test';

let chai = require('chai');
let chaiHttp = require('chai-http');
let server = require('../server');
let should = chai.should();
 
chai.use(chaiHttp);

function randomPersonnelID(length) {
    var result           = '';
    var characters       = '0123456789';
    var charactersLength = characters.length;
    for ( var i = 0; i < length; i++ ) {
       result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
 }

 function randomCode(length) {
    var result           = '';
    var characters       = '0123456789';
    var charactersLength = characters.length;
    for ( var i = 0; i < length; i++ ) {
       result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
 }

describe('USER TEST REQUESTS',()=>
{
    describe('/POST Register User Successfully and Failed',()=>{
        it('Register Successfully',(done)=>
        {
            let user = {
                "name":"CristinaTest",
                "mail":'cristina@gmail.com',
                "username":"CristinaTestUsername",
                "password":"cristinatest",
                "personnelID":randomPersonnelID(9),
                "company":"Continental"
            }
            chai.request(server)
            .post('/api/usersdb/register2')
            .send(user)
            .end((err,res)=>{
                res.should.have.status(200);
                done();
            })
        })
        it('Register Failed ', (done) => {
            let user = {
              "personnelID": undefined,
              "username": "",
            }
            chai.request(server)
              .post('/api/usersdb/register2')
              .send(user)
              .end((err, res) => {
                res.should.have.status(400);
                done();
              });
          });
    });
    
    describe('/POST Login User Successfully and Failed', () => {
        it('Login Successfully', (done) => {
          let user = {
            "username": "Bogdan123",
            "password": "continental",
            "personnelID": "123456789"
          }
          chai.request(server)
            .post('/api/usersdb/login2')
            .send(user)
            .end((err, res) => {
              res.should.have.status(200);
              done();
            });
        })
        it('Login Failed', (done) => {
            let user = {
                "username": "Bogdan",
                "password": "",
                "personnelID": "123456788",
            }
            chai.request(server)
              .post('/api/usersdb/login2')
              .send(user)
              .end((err, res) => {
                res.should.have.status(400);
                done();
              });
            })
    });
  
    describe('/POST Sick Leave Request Send and Failed',()=>{
        it('Sick Leave Request Send',(done)=>
        {
            let user = {
                "name":"CristinaTest",
                "personnelID":"900000000",
                "username":"CristinaTestUsername",
                "startdate":"2020-01-05",
                "enddate":"2020-01-07",
                "days":"2",
                "code":"2223"
            }
            chai.request(server)
            .post('/api/requestdb/request2')
            .send(user)
            .end((err,res)=>{
                res.should.have.status(200);
                done();
            })
        })
        it('Sick Leave Request Failed ', (done) => {
            let user = {
                "username": "Bogdan",
                "password": "",
                "personnelID": "123456788",
            }
            chai.request(server)
              .post('/api/requestdb/request2')
              .send(user)
              .end((err, res) => {
                res.should.have.status(400);
                done();
              });
          });
    });
    
})