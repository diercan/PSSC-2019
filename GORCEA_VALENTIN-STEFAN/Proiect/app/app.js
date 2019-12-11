'use strict';

const https = require('https');
const http = require('http');
const fs = require('fs');


const log = require('./app/services/logService');
const config = require('./app/services/configuration');
const dbService = new(require('./app/services/dbService'))(config);

const app = require('./app/controllers/mainController')(
    log,
    config,
    dbService
);

// PORT is either provided as cli param, or read from config
// =============================================================================
const HTTP_PORT = (process.argv)[2] || config.webServer.httpport;

// START THE SERVER
// =============================================================================
if (HTTP_PORT) {
    const server = http.createServer(app).listen(HTTP_PORT, function() {
        log.debug(`Server started on http port:  ${HTTP_PORT} ....`);
    });
    server.timeout = config.webServer.serverTimeout;
}

// SIGNAL HANDLERS
// =============================================================================
function gracefulShutdownHandler() {
    const msg = `Shutting down gracefully ...`;
    log.debug(msg);

    dataService.closeConnections().finally(() => {
        process.exit(0);
    });
}
process.on('SIGTERM', gracefulShutdownHandler);

process.on('unhandledRejection', (reason) => {
    log.debug(reason.stack || reason);
});

process.on('uncaughtException', (reason, promise) => {
    log.debug(reason.stack || reason);
});