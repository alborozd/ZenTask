

Date.prototype.toDate = function() {
    return this.toLocaleDateString() + " " + this.toLocaleTimeString();
}