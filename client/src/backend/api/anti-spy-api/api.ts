export async function GET(req: Request) {
  console.log("Log from GET.");
  return new Response("Response from GET.");
}

export async function POST(req: Request) {
  const data = await req.json();
  console.log("Log POST with body:", data);
  return Response.json(data);
}
