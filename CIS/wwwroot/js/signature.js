window.signaturePad = {
    canvas: null,
    ctx: null,
    drawing: false,

    init: function (canvasId) {
        this.canvas = document.getElementById(canvasId);
        if (!this.canvas) return;

        const rect = this.canvas.getBoundingClientRect();
        this.canvas.width = rect.width;
        this.canvas.height = rect.height;

        this.ctx = this.canvas.getContext('2d');
        this.ctx.lineWidth = 3;
        this.ctx.lineCap = 'round';
        this.ctx.lineJoin = 'round';
        this.ctx.strokeStyle = '#000000';

        // MOUSE EVENTS (Fixes drawing on desktop)
        this.canvas.addEventListener("mousedown", (e) => {
            this.start({ offsetX: e.offsetX, offsetY: e.offsetY });
        });
        this.canvas.addEventListener("mousemove", (e) => {
            this.draw({ offsetX: e.offsetX, offsetY: e.offsetY });
        });
        this.canvas.addEventListener("mouseup", () => this.stop());
        this.canvas.addEventListener("mouseleave", () => this.stop());

        // TOUCH EVENTS (For mobile)
        this.canvas.addEventListener("touchstart", (e) => {
            const rect = this.canvas.getBoundingClientRect();
            const touch = e.touches[0];
            this.start({ offsetX: touch.clientX - rect.left, offsetY: touch.clientY - rect.top });
            e.preventDefault();
        }, { passive: false });

        this.canvas.addEventListener("touchmove", (e) => {
            const rect = this.canvas.getBoundingClientRect();
            const touch = e.touches[0];
            this.draw({ offsetX: touch.clientX - rect.left, offsetY: touch.clientY - rect.top });
            e.preventDefault();
        }, { passive: false });

        this.canvas.addEventListener("touchend", () => this.stop(), false);
    },

    start: function (pos) {
        this.drawing = true;
        this.ctx.beginPath();
        this.ctx.moveTo(pos.offsetX, pos.offsetY);
    },

    draw: function (pos) {
        if (!this.drawing) return;
        this.ctx.lineTo(pos.offsetX, pos.offsetY);
        this.ctx.stroke();
    },

    stop: function () { this.drawing = false; },

    clear: function () {
        if (this.ctx) this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    },

    getData: function () { return this.canvas.toDataURL('image/png'); }
};