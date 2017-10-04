var path = require("path");

var baseConfiguration = {
    pattern: [
        '**/serialization/Templates/**/*.yml',
        '**/serialization/*Templates/**/*.yml'
    ],
    templatePath: path.join(__dirname, 'codegeneration.config.tmpl')
}

var getNamespace = (dir) => {
    return dir.split(path.sep)
        .slice(-3, -1)
        .map(str => str
            .replace(/(?:^\w|[A-Z]|\b\w)/g, (match) => {
                return match.toUpperCase();
            })
            .replace(/[^a-zA-Z\d]+/g, '')
        )
        .join('.')
}

module.exports = (dir) => {
    return {
        ...baseConfiguration,
        cwd: path.join(dir, '..'),
        CommonNamespace: getNamespace(dir)
    }
}