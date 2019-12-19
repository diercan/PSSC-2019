'use strict';

const http = require('http');
const log = require('./app/middlewares/logService');
const config = require('./app/middlewares/configuration');
const db = new(require('./app/models/db'))(config);
const userModel = new(require('./app/models/userModel'))(config,db);

const app = require('./app/controllers/mainController')(
    log,
    config,
    db,
    userModel
);

// PORT is either provided as cli param, or read from config
// =============================================================================
const HTTP_PORT = (process.argv)[2] || config.webServer.httpport;

// START THE SERVER
// =============================================================================
if (HTTP_PORT) {
    const server = http.createServer(app).listen(HTTP_PORT, async function() {
            log.debug(`Server started on http port:  ${HTTP_PORT} ....`);
        
    });
    server.timeout = config.webServer.serverTimeout;
}

// SIGNAL HANDLERS
// =============================================================================
function gracefulShutdownHandler() {
    const msg = `Shutting down gracefully ...`;
    //log.debug(msg);

    dataService.closeConnections().finally(() => {
        process.exit(0);
    });
}
process.on('SIGTERM', gracefulShutdownHandler);

process.on('unhandledRejection', (reason) => {
   // log.debug(reason.stack || reason);
});

process.on('uncaughtException', (reason, promise) => {
//log.debug(reason.stack || reason);
});