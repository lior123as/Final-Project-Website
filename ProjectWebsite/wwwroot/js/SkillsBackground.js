let canvas, ctx, nodes = [], connections = [], flow = 0;

export function updateLines(n, c) {
    canvas = document.getElementById('connectionsCanvas');
    if (!canvas) return;
    ctx = canvas.getContext('2d');
    nodes = n;
    connections = c;
    const resize = () => { canvas.width = window.innerWidth; canvas.height = window.innerHeight; };
    window.addEventListener('resize', resize);
    resize();
    requestAnimationFrame(renderLoop);
}

window.updateLines = updateLines;

function renderLoop() {
    if (!ctx) return;
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    const container = document.getElementById('zoom-container');
    if (!container) { requestAnimationFrame(renderLoop); return; }

    const matrix = new DOMMatrixReadOnly(window.getComputedStyle(container).transform);
    ctx.save();
    ctx.setTransform(matrix.a, matrix.b, matrix.c, matrix.d, matrix.e, matrix.f);

    flow = (flow + 0.005) % 1;

    connections.forEach(con => {
        const sNode = nodes.find(n => n.s.id === con.skill);
        const eNode = nodes.find(n => n.s.id === con.skillAfter);
        if (!sNode || !eNode) return;

        // Modern RPG Data-Link (Tapered glowing line)
        const grad = ctx.createLinearGradient(sNode.x, sNode.y, eNode.x, eNode.y);
        grad.addColorStop(0, 'rgba(0, 242, 255, 0.05)');
        grad.addColorStop(0.5, 'rgba(0, 242, 255, 0.2)');
        grad.addColorStop(1, 'rgba(0, 242, 255, 0.05)');

        ctx.beginPath();
        ctx.strokeStyle = grad;
        ctx.lineWidth = 2;
        ctx.moveTo(sNode.x, sNode.y);
        ctx.lineTo(eNode.x, eNode.y);
        ctx.stroke();

        // Data Pulse Particles
        const dx = eNode.x - sNode.x, dy = eNode.y - sNode.y;
        for (let i = 0; i < 2; i++) {
            const p = (flow + (i * 0.5)) % 1;
            ctx.fillStyle = `rgba(255, 255, 255, ${Math.sin(p * Math.PI)})`;
            ctx.beginPath();
            ctx.arc(sNode.x + dx * p, sNode.y + dy * p, 2, 0, Math.PI * 2);
            ctx.fill();
        }
    });

    ctx.restore();
    requestAnimationFrame(renderLoop);
}
