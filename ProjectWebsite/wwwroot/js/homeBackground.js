// homeBackground.js - 2026 Flight Edition
let canvas, ctx, width, height;
let mountains = [];
let speed = 4;

export function initMountainBackground() {
    canvas = document.getElementById('mountain-canvas');
    if (!canvas) return;
    ctx = canvas.getContext('2d');

    const resize = () => {
        width = window.innerWidth;
        height = window.innerHeight;
        canvas.width = width;
        canvas.height = height;
    };
    window.addEventListener('resize', resize);
    resize();
    animate();
}

class Mountain {
    constructor() {
        this.z = 1200;
        this.x = (Math.random() - 0.5) * width * 3;
        this.y = height * 0.6;
        this.color = `rgb(0, ${Math.random() * 20 + 5}, ${Math.random() * 40 + 10})`;
    }
    update() { this.z -= speed; return this.z > 0; }
    draw() {
        const scale = 1000 / (this.z || 1);
        const sx = width / 2 + this.x * (scale / 10);
        const sy = height / 2 + (this.y - height / 2) * scale;
        const w = width * scale;

        ctx.beginPath();
        ctx.moveTo(sx - w / 2, height);
        ctx.lineTo(sx, sy);
        ctx.lineTo(sx + w / 2, height);
        ctx.fillStyle = this.color;
        ctx.fill();
        ctx.strokeStyle = `rgba(0, 255, 204, ${0.05 * scale})`;
        ctx.stroke();
    }
}

function animate() {
    ctx.fillStyle = "#000";
    ctx.fillRect(0, 0, width, height);
    if (Math.random() < 0.08) mountains.push(new Mountain());
    mountains = mountains.filter(m => m.update());
    mountains.sort((a, b) => b.z - a.z).forEach(m => m.draw());
    requestAnimationFrame(animate);
}

window.triggerRotationFlight = (pageName) => {
    // 1. Display destination name
    const overlay = document.getElementById('transition-overlay');
    overlay.innerText = pageName;
    overlay.style.transition = "opacity 0.3s, transform 0.8s";
    overlay.style.opacity = "1";
    overlay.style.transform = "translate(-50%, -50%) scale(1.5)";

    // 2. Perform 3D "Flight through wall"
    document.body.style.perspective = "1200px";
    document.body.style.transform = "rotateY(30deg) translateZ(1500px) scale(0)";
    document.body.style.opacity = "0";

    // 3. Collision Wave
    const wave = document.createElement('div');
    wave.style.cssText = `
        position:fixed; top:50%; left:50%; transform:translate(-50%,-50%);
        border:2px solid #00ffcc; border-radius:50%; pointer-events:none; z-index:9999;
        animation: impact 0.8s ease-out forwards;
    `;
    document.body.appendChild(wave);
};

// CSS Injection for impact wave
const style = document.createElement('style');
style.innerHTML = `
    @keyframes impact {
        0% { width:0; height:0; opacity:1; }
        100% { width:300vw; height:300vw; opacity:0; }
    }
`;
document.head.appendChild(style);
