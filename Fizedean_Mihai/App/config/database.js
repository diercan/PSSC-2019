if (process.env.NODE_ENV === 'production') {
    module.exports = {
        mongoURI: 'mongodb+srv://mihai:mihai@vidjot-95lrp.gcp.mongodb.net/test?retryWrites=true&w=majority'
    }
} else {
    module.exports = {
        mongoURI: 'mongodb+srv://mihai:mihai@vidjot-95lrp.gcp.mongodb.net/test?retryWrites=true&w=majority'
    }
}