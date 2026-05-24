/**
 * registerBackground.js - 2026 Edition
 * Cyan Lightning Strikes targeting the central card.
 */
let canvas, ctx, width, height;
let bolts = [];

// Keep the export for module compatibility
export function initLightningBackground(canvasId) {
    canvas = document.getElementById(canvasId);
    if (!canvas) return;
    ctx = canvas.getContext('2d');

    const resize = () => {
        width = canvas.width = window.innerWidth;
        height = canvas.height = window.innerHeight;
    };
    window.addEventListener('resize', resize);
    resize();
    window.addEventListener('resize', resize);
    animate();
}

function resize() {
    width = canvas.width = window.innerWidth;
    height = canvas.height = window.innerHeight;
}

class Lightning {
    constructor() {
        this.reset();
    }

    reset() {
        // Bolts strike from the screen edges
        const side = Math.floor(Math.random() * 4);
        if (side === 0) { this.x = Math.random() * width; this.y = 0; }
        else if (side === 1) { this.x = Math.random() * width; this.y = height; }
        else if (side === 2) { this.x = 0; this.y = Math.random() * height; }
        else { this.x = width; this.y = Math.random() * height; }

        // Targeted at the center where the registration card sits
        this.tx = width / 2 + (Math.random() - 0.5) * 150;
        this.ty = height / 2 + (Math.random() - 0.5) * 150;

        this.life = 0;
        this.maxLife = 10 + Math.random() * 20;
        this.path = [{ x: this.x, y: this.y }];
        this.alpha = Math.random() * 0.4 + 0.4;
    }

    update() {
        this.life++;
        let last = this.path[this.path.length - 1];

        // High-velocity jagged movement toward target
        let dx = (this.tx - last.x) / (this.maxLife - this.life);
        let dy = (this.ty - last.y) / (this.maxLife - this.life);

        this.path.push({
            x: last.x + dx + (Math.random() - 0.5) * 50,
            y: last.y + dy + (Math.random() - 0.5) * 50
        });

        if (this.life >= this.maxLife) this.reset();
    }

    draw() {
        ctx.beginPath();
        // Cyan Lightning Theme
        ctx.strokeStyle = `rgba(0, 255, 249, ${this.alpha})`;
        ctx.lineWidth = 1.5;
        ctx.shadowBlur = 10;
        ctx.shadowColor = "#00fff9";

        ctx.moveTo(this.path[0].x, this.path[0].y);
        for (let i = 1; i < this.path.length; i++) {
            ctx.lineTo(this.path[i].x, this.path[i].y);
        }
        ctx.stroke();
    }
}

function animate() {
    // Pure Black Background with faint trail preservation
    ctx.fillStyle = "rgba(0, 0, 0, 1)";
    ctx.fillRect(0, 0, width, height);

    if (bolts.length < 12) bolts.push(new Lightning());

    bolts.forEach(b => {
        b.update();
        b.draw();
    });

    requestAnimationFrame(animate);
}

window.initLightningBackground = initLightningBackground;
